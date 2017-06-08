using System;
using System.Linq;
using System.Web.Hosting;
using Lottery.DataUpdater.Models;
using Lottery.Engine;
using LotteryService.Application.Lottery;
using LotteryService.Common.Enums;
using LotteryService.Domain.Logs;
using Microsoft.Practices.ServiceLocation;
using Quartz;

namespace Lottery.DataUpdater.Jobs
{
    public abstract class LotteryDataJob : IJob,IRegisteredObject
    {
        private readonly object _lock = new object();
        protected readonly LotteryType _LotteryType;
        private bool _shuttingDown;
        private readonly ILotteryDataAppService _lotteryDataAppService;
        private readonly ILotteryUpdateConfigLoader _lotteryUpdateConfigLoader;
        protected readonly LotteryUpdateConfig _lotteryUpdateConfig;

        protected static bool isFirstStartService = true;

        protected DataUpdateContainer _dataUpdateContainer;
        protected static DateTime _nextLotteryTime;

        protected LotteryDataJob(LotteryType lotteryType)
        {
            HostingEnvironment.RegisterObject(this);
            _LotteryType = lotteryType;
            _lotteryDataAppService = ServiceLocator.Current.GetInstance<ILotteryDataAppService>();
            _lotteryUpdateConfigLoader = ServiceLocator.Current.GetInstance<ILotteryUpdateConfigLoader>();
            _lotteryUpdateConfig = _lotteryUpdateConfigLoader.GetLotteryUpdateConfigs().Single(p=>p.Name == lotteryType.ToString());
            //_nextLotteryTime = _lotteryUpdateConfig.NextLotteryTime;

            _dataUpdateContainer = new DataUpdateContainer(_lotteryUpdateConfig,_lotteryDataAppService,this);
            
        }

        internal DateTime NextLotteryTime
        {
            get
            {
                if (_nextLotteryTime == DateTime.MinValue)
                {
                    _nextLotteryTime = _lotteryUpdateConfig.NextLotteryTime;
                }
                return _nextLotteryTime;
            }
            set { _nextLotteryTime = value; }
        }

        internal LotteryType LotteryType
        {
            get { return _LotteryType; }
        }

        internal bool IsFirstStartService
        {
            get { return isFirstStartService; }
        }

        public void Execute(IJobExecutionContext context)
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                try
                {
                    if (isFirstStartService)
                    {
                        var lotteryEngine = LotteryEngine.GetLotteryEngine(LotteryType.bjpks);
                        isFirstStartService = false;
                        LogDbHelper.LogInfo(string.Format("{0}彩种定时执行任务Job开始(First Start)", _LotteryType),
                            GetType().FullName + "=>Start", OperationType.Job);
                        _dataUpdateContainer.Execute();
                        
                    }
                    else
                    {         
                        if (DateTime.Now > NextLotteryTime ||
                            NextLotteryTime - DateTime.Now < TimeSpan.FromSeconds(_lotteryUpdateConfig.Interval * 3))
                        {
                            _dataUpdateContainer.Execute();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    LogDbHelper.LogFatal(ex, GetType() + "Execute", OperationType.Job);
                }

            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }
            LogDbHelper.LogInfo(string.Format("{0}彩种定时执行任务Job结束",_LotteryType),
                GetType().FullName + "=>Stop",OperationType.Job);
             HostingEnvironment.UnregisterObject(this);
        }

       
    }
}
using System;
using System.IO;
using log4net;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;

namespace LotteryService.Domain.Logs
{
    public class LogDbHelper
    {
        private static string _loggerName = "DbLogger";

        private static ILog _dbLog;

        public static ILog DbLog
        {
            get
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.db.config"));

                if (_dbLog == null)
                {
                    _dbLog = log4net.LogManager.GetLogger(_loggerName);
                }
                else
                {
                    if (_dbLog.Logger.Name != _loggerName)
                    {
                        _dbLog = log4net.LogManager.GetLogger(_loggerName);
                    }
                }
                return _dbLog;
            }
        }

        public static void LogError(string errorMsg,string methodName)
        {
            var operationType = GetOperationType(methodName);

            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = errorMsg,

            };

            if (DbLog.IsErrorEnabled)
            {
                DbLog.Error(errorLog);
            }
        }

        public static void LogError(Exception ex, string methodName, OperationType operationType)
        {
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = ex.Message,
                Exception = ex.ToString()
            };

            if (DbLog.IsErrorEnabled)
            {
                DbLog.Error(errorLog);
            }
        }

        public static void LogError(Exception ex, string methodName)
        {
            var operationType = GetOperationType(methodName);
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = ex.Message,
                Exception = ex.ToString()

            };
            if (DbLog.IsErrorEnabled)
            {
                DbLog.Error(errorLog);
            }
        }

        private static OperationType GetOperationType(string methodName) 
        {
            var operationType = OperationType.Other;
            if (methodName.ToLower().Contains("add") || methodName.ToLower().Contains("creat") ||
                methodName.ToLower().Contains("save") || methodName.ToLower().Contains("insert"))
            {
                operationType = OperationType.Save;
            }

            if (methodName.ToLower().Contains("delete") || methodName.ToLower().Contains("del"))
            {
                operationType = OperationType.Delete;
            }

            if (methodName.ToLower().Contains("update") || methodName.ToLower().Contains("modify"))
            {
                operationType = OperationType.Update;
            }

            if (methodName.ToLower().Contains("query") || methodName.ToLower().Contains("select") ||
                methodName.ToLower().Contains("get") || methodName.ToLower().Contains("all"))
            {
                operationType = OperationType.Query;
            }
            return operationType;
        }

        public static void LogInfo(string info, string methodName)
        {
            var operationType = GetOperationType(methodName);
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = info,

            };

            if (DbLog.IsInfoEnabled)
            {
                DbLog.Info(errorLog);
            }
        }       

        public static void LogInfo(string info, string methodName, OperationType operationType)
        {
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = info,

            };

            if (DbLog.IsInfoEnabled)
            {
                DbLog.Info(errorLog);
            }
        }

        public static void LogWarn(string info, string methodName)
        {
            var operationType = GetOperationType(methodName);
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = info,

            };

            if (DbLog.IsWarnEnabled)
            {
                DbLog.Warn(errorLog);
            }
        }

        public static void LogWarn(string info, string methodName, OperationType operationType)
        {
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = info,

            };

            if (DbLog.IsWarnEnabled)
            {
                DbLog.Warn(errorLog);
            }
        }

        public static void LogFatal(string fatalMsg, string methodName)
        {
            var operationType = GetOperationType(methodName);

            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = fatalMsg,

            };

            if (DbLog.IsFatalEnabled)
            {
                DbLog.Fatal(errorLog);
            }
        }

        public static void LogFatal(Exception ex, string methodName, OperationType operationType)
        {
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = ex.Message,
                Exception = ex.ToString()
            };

            if (DbLog.IsFatalEnabled)
            {
                DbLog.Fatal(errorLog);
            }
        }

        public static void LogFatal(Exception ex, string methodName)
        {
            var operationType = GetOperationType(methodName);
            var errorLog = new ErrorLog()
            {
                OperationType = operationType.ToString(),
                Logger = _loggerName,
                MethodName = methodName,
                IP = IpHelper.GetClientIP(),
                UserId = 0,
                Message = ex.Message,
                Exception = ex.ToString()

            };
            if (DbLog.IsFatalEnabled)
            {
                DbLog.Fatal(errorLog);
            }
        }
    }
}
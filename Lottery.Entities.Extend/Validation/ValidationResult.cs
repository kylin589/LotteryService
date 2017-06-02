using LotteryService.Common.Excetions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Entities.Extend.Validation
{
    public class ValidationResult
    {
        private readonly List<ValidationError> _erros;
        private IDictionary<string, object> _datas;

        private string Message { get; set; }

        public bool IsValid { get { return !_erros.Any(); } }

       

        public IEnumerable<ValidationError> Errors { get { return _erros; } }

        public IDictionary<string, object> Datas {
            get { return _datas; }
        }

        public ValidationResult()
        {
            _erros = new List<ValidationError>();
            _datas = new Dictionary<string,object>();
        }

        public void SetData<T>(string key, T data)
        {
            if (!_datas.ContainsKey(key))
            {
                _datas.Add(key, data);
            }
            else
            {
                _datas[key] = data;
            }
        }

        public T GetData<T>(string key)
        {
            if (!IsSet(key))
            {
                throw new LSException(string.Format("不存在key为{0}的数据",key));
            }
            object o = null;
            _datas.TryGetValue(key, out o);
            if (o == null)
            {
                return default(T);
            }
            return (T)o;
        }

        public bool IsSet(string key)
        {
            object o = null;
            _datas.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        public ValidationResult Add(string errorMessage)
        {
            _erros.Add(new ValidationError(errorMessage));
            return this;
        }

        public ValidationResult Add(ValidationError error)
        {
            _erros.Add(error);
            return this;
        }

        public ValidationResult Add(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return this;

            foreach (var result in validationResults.Where(r => r != null))
            {
                _erros.AddRange(result.Errors);
                foreach (var data in result.Datas)
                {
                    this.SetData(data.Key, data.Value);
                }
            }

            return this;
        }



        public ValidationResult Remove(ValidationError error)
        {
            if (_erros.Contains(error))
                _erros.Remove(error);
            return this;
        }
    }
}
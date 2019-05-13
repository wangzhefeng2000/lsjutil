﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Lsj.Util.WPF
{
    /// <summary>
    /// Model Object
    /// </summary>
    public class ModelObject : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                var vc = new ValidationContext(this);
                vc.MemberName = columnName;
                var result = new List<ValidationResult>();

                var value = this.GetType().GetProperty(columnName)?.GetValue(this, null);
                if (value != null)
                {
                    Validator.TryValidateProperty(value, vc, result);
                    var stringResult = result.Select(x => x.ErrorMessage).ToList();
                    this.Validate(columnName, value, stringResult);
                    if (stringResult.Count > 0)
                    {
                        return string.Join(Environment.NewLine, stringResult);
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="name">Property Name</param>
        /// <param name="value">Property Value</param>
        /// <param name="result">Result</param>
        protected virtual void Validate(string name, object value, List<string> result)
        {
        }

        /// <summary>
        /// Error
        /// </summary>
        public virtual string Error => "";

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="affectedPropertyNames"></param>
        /// <param name="propertyName"></param>
        protected void SetField<T>(ref T field, T value, string[] affectedPropertyNames = null, [CallerMemberName]string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                this.OnPropertyChanged(propertyName);
            }
            if (affectedPropertyNames != null && affectedPropertyNames.Length > 0)
            {
                foreach (var name in affectedPropertyNames)
                {
                    this.OnPropertyChanged(name);
                }
            }
        }
    }
}
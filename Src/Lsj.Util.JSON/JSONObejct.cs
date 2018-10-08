﻿using Lsj.Util.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Lsj.Util.JSON
{
    /// <summary>
    /// JSON Object
    /// </summary>
    public class JSONObejct : DynamicObject
    {
        private readonly SafeDictionary<string, object> data = new SafeDictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            if (this.data.ContainsKey(name))
            {
                result = this.data[name];
                return true;
            }
            else
            {
                return base.TryGetMember(binder, out result);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this.data[binder.Name] = value;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {

            if (binder.Name == "Set" && args.Length == 2 && args[0] is string)
            {
                result = null;
                this.data[(string)args[0]] = args[1];
                return true;
            }
            else
            {
                return base.TryInvokeMember(binder, args, out result);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetDynamicMemberNames() => this.data.Keys;
    }
}

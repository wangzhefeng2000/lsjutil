﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NETCOREAPP1_1
namespace Lsj.Util.Core.Protobuf
#else
namespace Lsj.Util.Protobuf
#endif
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute :Attribute
    {

        public FieldAttribute(int FieldNumber, eFieldType FieldType = eFieldType.Varint)
        {
            if (FieldNumber < 1 || FieldNumber > 2047)
            {
                throw new ArgumentOutOfRangeException("FieldNumber Out Of Range");
            }
            this.FieldNumber = FieldNumber;
            this.FieldType = FieldType;
        }
        public int FieldNumber
        {
            get;
        }
        public eFieldType FieldType
        {
            get;
        } = eFieldType.Varint;

    }
}
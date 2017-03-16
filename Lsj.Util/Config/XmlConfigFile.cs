﻿using Lsj.Util.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lsj.Util.Reflection;
using Lsj.Util.Text;

namespace Lsj.Util.Config
{
    /// <summary>
    /// XML Confg File
    /// </summary>
    public class XmlConfigFile : XmlFile
    {
        /// <summary>
        /// Initialize a new instance with a path
        /// </summary>
        /// <param name="path"></param>
        public XmlConfigFile(string path) : base(path)
        {
            Refresh();
        }
        /// <summary>
        /// Refresh
        /// </summary>
        public sealed override void Refresh()
        {
            base.Refresh();
            if (m_Document.HasChildNodes)
            {
                var config = m_Document.DocumentElement.SelectSingleNode("/config");
                if (config != null && config.HasChildNodes)
                {
                    var fields = this.GetType().GetAllNonPublicField();
                    foreach (var field in fields)
                    {
                        if (field.FieldType.IsAssignableFrom(typeof(ConfigElement)))
                        {
                            var attribute = field.GetAttribute<ConfigElementNameAttribute>();
                            if (attribute != null)
                            {
                                var name = attribute.Name.ToSafeString();
                                if (name != "")
                                {
                                    var element = config.SelectSingleNode(name);
                                    if (element != null)
                                    {
                                        field.SetValue(this, new ConfigElement(element.InnerText));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
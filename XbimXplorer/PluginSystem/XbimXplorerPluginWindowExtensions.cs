﻿using System;
using System.Linq;
using System.Reflection;
using log4net;
using Xbim.Presentation.XplorerPluginSystem;

namespace XbimXplorer.PluginSystem
{
    internal static class XbimXplorerPluginWindowExtensions
    {
        private static readonly ILog Log = LogManager.GetLogger("XbimXplorer.PluginSystem.XbimXplorerPluginWindowExtensions");

        internal static PluginWindowUiContainerEnum GetUiContainerMode(this Type pluginType)
        {
            var attribute = pluginType.GetUiAttribute();
            return DefaultContainer(attribute);
        }

        internal static PluginWindowActivation GetUiActivation(this Type pluginType)
        {
            var attribute = pluginType.GetUiAttribute();
            return DefaultActivation(attribute);
        }

        internal static PluginWindowUiContainerEnum GetUiContainerMode(this IXbimXplorerPluginWindow pluginWindow)
        {
            var attribute = pluginWindow.GetUiAttribute();
            return DefaultContainer(attribute);
        }
        
        internal static PluginWindowActivation GetUiActivation(this IXbimXplorerPluginWindow pluginWindow)
        {
            var attribute = pluginWindow.GetUiAttribute();
            return DefaultActivation(attribute);
        }

        internal static XplorerUiElement GetUiAttribute(this Type pluginWindowType)
        {
            MemberInfo info = pluginWindowType;
            return GetUiAttribute(info);
        }

        internal static XplorerUiElement GetUiAttribute(this IXbimXplorerPluginWindow pluginWindow)
        {
            return GetUiAttribute(pluginWindow.GetType());
        }

        private static XplorerUiElement GetUiAttribute(MemberInfo info)
        {
            var attribute = info.GetCustomAttributes(true).OfType<XplorerUiElement>().FirstOrDefault();
            if (attribute == null)
            {
                Log.InfoFormat("XplorerUiElement attribute is null on type: {0}", info.Name);
            }
            return attribute;
        }

        private static PluginWindowUiContainerEnum DefaultContainer(XplorerUiElement attribute)
        {
            var useContainer = attribute != null
                ? attribute.InitialContainer
                : PluginWindowUiContainerEnum.LayoutDoc;
            return useContainer;
        }

        private static PluginWindowActivation DefaultActivation(XplorerUiElement attribute)
        {
            var useContainer = attribute != null
                ? attribute.Activation
                : PluginWindowActivation.OnLoad;
            return useContainer;
        }
    }
}

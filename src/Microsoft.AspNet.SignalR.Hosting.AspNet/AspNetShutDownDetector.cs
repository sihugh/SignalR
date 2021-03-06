﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.md in the project root for license information.

using System;
using System.Diagnostics;
using System.Web.Hosting;

namespace Microsoft.AspNet.SignalR.Hosting.AspNet
{
    internal class AspNetShutDownDetector : IRegisteredObject
    {
        private readonly Action _onShutdown;

        public AspNetShutDownDetector(Action onShutdown)
        {
            _onShutdown = onShutdown;
        }

        public void Initialize()
        {
            try
            {
                HostingEnvironment.RegisterObject(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Stop(bool immediate)
        {
            try
            {
                _onShutdown();
            }
            catch
            {
                // Swallow the exception as Stop should never throw
                // TODO: Log exceptions
            }
            finally
            {
                HostingEnvironment.UnregisterObject(this);
            }
        }
    }
}

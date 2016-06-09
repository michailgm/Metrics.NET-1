﻿using System;
using System.Collections.Generic;
using System.Net;
using Metrics.MetricData;
using Metrics.Visualization;

namespace Metrics.Reports
{
    public sealed class MetricsEndpointReports : Utils.IHideObjectMembers
    {
        private readonly MetricsDataProvider metricsDataProvider;
        private readonly Func<HealthStatus> healthStatus;

        private readonly List<MetricsEndpoint> endpoints = new List<MetricsEndpoint>();

        internal IEnumerable<MetricsEndpoint> Endpoints => this.endpoints;

        public MetricsEndpointReports(MetricsDataProvider metricsDataProvider, Func<HealthStatus> healthStatus)
        {
            this.metricsDataProvider = metricsDataProvider;
            this.healthStatus = healthStatus;
        }

        public MetricsEndpointReports WithEndpointReport(string endpoint, Func<MetricsData, Func<HealthStatus>, HttpListenerContext, MetricsEndpointResponse> responseFactory)
        {
            var metricsEndpoint = new MetricsEndpoint(endpoint, (c) => responseFactory(this.metricsDataProvider.CurrentMetricsData, this.healthStatus, c));
            this.endpoints.Add(metricsEndpoint);
            return this;
        }
    }
}

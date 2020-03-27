﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TraceDefense.Entities;
using TraceDefense.Entities.Geospatial;

namespace TraceDefense.DAL.Repositories
{
    /// <summary>
    /// <see cref="Query"/> repository definition
    /// </summary>
    public interface IQueryRepository : IRepository<Query>
    {
        /// <summary>
        /// Pulls a collection of query data based on unique identifiers
        /// </summary>
        /// <param name="queryIds">Collection of <see cref="Query"/> identifiers to pull</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of <see cref="Query"/> data</returns>
        Task<IList<Query>> GetQueriesAsync(RegionRef region, int lastTimestamp, CancellationToken cancellationToken = default);
        /// <summary>
        /// Stores a new <see cref="Query"/>
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="query"><see cref="Query"/> content</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>New <see cref="Query"/> identifier</returns>
        Task PublishAsync(IList<RegionRef> regions,  Query query, CancellationToken cancellationToken = default);
    }
}
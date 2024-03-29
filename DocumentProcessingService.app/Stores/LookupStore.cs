﻿using DocumentProcessingService.app.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentProcessingService.app.Stores
{
    public interface ILookupStore
    {
        /// <summary> 
        /// Records set of keywords identified in the given document for a given client 
        /// </summary> 
        /// <param name="client">Client identifier</param> 
        /// <param name="documentId">Document identifier</param> 
        /// <param name="keywords">Enumeration of unique keywords found in the document, in any
        /// order. Only match exact words, not prefix match. </param> 
        Task Record(string client, string documentId, IEnumerable<string> keywords);
    }

    public class LookupStore : ILookupStore
    {
        private readonly DocumentContext _context;
        private readonly ILogger<LookupStore> _logger;

        public LookupStore(DocumentContext context, ILogger<LookupStore> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Record(string client, string documentId, IEnumerable<string> keywords)
        {
            if (string.IsNullOrWhiteSpace(client) || string.IsNullOrWhiteSpace(documentId) || keywords == null)
            {
                _logger.LogWarning($"Invalid input, keywords not persisted for client {client}, documentId {documentId}");
                return;
            }

            var documentItem = new DocumentItem
            {
                Client = client,
                DocumentId = documentId,
                Keywords = string.Join(',', keywords)
            };

            await _context.DocumentItems.AddAsync(documentItem);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully persisted result for client: {client}, document: {documentId}");
        }
    }
}

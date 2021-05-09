﻿using System.Collections.Generic;

namespace DocumentProcessingService.app.Models
{
    public class DocumentContent
    {
        public string ProcessingType { get; set; }
        public IEnumerable<string> Parameters { get; set; }
        public string Body { get; set; }
    }
}

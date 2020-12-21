﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Operations
{
    public class OperationCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
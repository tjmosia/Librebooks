﻿using MacbooksAPI.CoreLib.Operations;

namespace MacbooksAPI.Core.EFCore
{
    public class DbError
    {
        public int ErrorNumber { get; set; }
        public TransactionError? Error { get; set; }
    }
}

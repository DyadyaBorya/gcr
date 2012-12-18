using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Transactions;

namespace GCR.Core
{
    /// <summary>
    /// This class contains methods for using the transaction scope.
    /// </summary>
    public class TransactionScope : IDisposable
    {
        /// <summary>
        /// Current transaction scope.
        /// </summary>
        private System.Transactions.TransactionScope scope;

        /// <summary>
        /// Initializes a new instance of the TransactionScope class.
        /// </summary>
        public TransactionScope()
            : this(TransactionScopeOption.Required)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the TransactionScope class
        ///     and sets the specified transaction as the ambient transaction, so that transactional
        ///     work done inside the scope uses this transaction.
        /// </summary>
        /// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional
        ///     work done inside the scope uses this transaction.
        /// </param>
        public TransactionScope(Transaction transactionToUse)
        {
            this.scope = new System.Transactions.TransactionScope(transactionToUse);
        }

        /// <summary>
        ///  Initializes a new instance of the TransactionScope class with the specified requirements.
        /// </summary>
        /// <param name="scopeOption">
        ///     An instance of the System.Transactions.TransactionScopeOption enumeration
        ///     that describes the transaction requirements associated with this transaction
        ///     scope.
        /// </param>
        public TransactionScope(TransactionScopeOption scopeOption)
            : this(scopeOption, GetDefaultOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the TransactionScope class
        ///     with the specified timeout value, and sets the specified transaction as the
        ///     ambient transaction, so that transactional work done inside the scope uses
        ///     this transaction.
        /// </summary>
        /// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional
        ///     work done inside the scope uses this transaction.</param>
        /// <param name="scopeTimeout"> The System.TimeSpan after which the transaction scope times out and aborts
        ///     the transaction.
        /// </param>
        public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout)
        {
            this.scope = new System.Transactions.TransactionScope(transactionToUse, scopeTimeout);
        }

        /// <summary>
        /// Initializes a new instance of the TransactionScope class
        ///     with the specified timeout value and requirements.
        /// </summary>
        /// <param name="scopeOption"> An instance of the TransactionScopeOption enumeration
        ///     that describes the transaction requirements associated with this transaction
        ///     scope.
        /// </param>
        /// <param name="scopeTimeout">The System.TimeSpan after which the transaction scope times out and aborts
        ///     the transaction.
        /// </param>
        public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout)
            : this(scopeOption, GetDefaultOptions(scopeTimeout))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TransactionScope class
        ///     with the specified requirements.
        /// </summary>
        /// <param name="scopeOption">An instance of the TransactionScopeOption enumeration
        ///     that describes the transaction requirements associated with this transaction
        ///     scope.
        /// </param>
        /// <param name="transactionOptions"> A TransactionOptions structure that describes the transaction
        ///     options to use if a new transaction is created. If an existing transaction
        ///     is used, the timeout value in this parameter applies to the transaction scope.
        ///     If that time expires before the scope is disposed, the transaction is aborted.
        /// </param>
        public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
        {
            this.scope = new System.Transactions.TransactionScope(scopeOption, transactionOptions);
        }

        /// <summary>
        ///  Initializes a new instance of the TransactionScope class
        ///     with the specified timeout value and COM+ interoperability requirements,
        ///     and sets the specified transaction as the ambient transaction, so that transactional
        ///     work done inside the scope uses this transaction.
        /// </summary>
        /// <param name="transactionToUse"> The transaction to be set as the ambient transaction, so that transactional
        ///     work done inside the scope uses this transaction.</param>
        /// <param name="scopeTimeout">The System.TimeSpan after which the transaction scope times out and aborts
        ///     the transaction.</param>
        /// <param name="interopOption"> An instance of the EnterpriseServicesInteropOption enumeration
        ///     that describes how the associated transaction interacts with COM+ transactions.
        /// </param>
        public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
        {
            this.scope = new System.Transactions.TransactionScope(transactionToUse, scopeTimeout, interopOption);
        }

        /// <summary>
        /// Initializes a new instance of the TransactionScope class
        ///     with the specified scope and COM+ interoperability requirements, and transaction
        ///     options.
        /// </summary>
        /// <param name="scopeOption">An instance of the TransactionScopeOption enumeration
        ///     that describes the transaction requirements associated with this transaction
        ///     scope.</param>
        /// <param name="transactionOptions">A TransactionOptions structure that describes the transaction
        ///     options to use if a new transaction is created. If an existing transaction
        ///     is used, the timeout value in this parameter applies to the transaction scope.
        ///     If that time expires before the scope is disposed, the transaction is aborted.</param>
        /// <param name="interopOption">An instance of the EnterpriseServicesInteropOption enumeration
        ///     that describes how the associated transaction interacts with COM+ transactions.
        /// </param>
        public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
        {
            this.scope = new System.Transactions.TransactionScope(scopeOption, transactionOptions, interopOption);
        }

        /// <summary>
        /// Get TransactionOptions with Default IsolationLevel and Timeout
        /// </summary>
        /// <returns>Transaction scope options.</returns>
        public static TransactionOptions GetDefaultOptions()
        {
            return GetDefaultOptions(TimeSpan.MinValue);
        }

        /// <summary>
        /// Get TransactionOptions with Default IsolationLevel and specified Timeout
        /// (use TimeSpan.MinValue to use the Default Timeout)
        /// </summary>
        /// <param name="timeout">Timespan of the scope.</param>
        /// <returns>Transaction scope options.</returns>
        public static TransactionOptions GetDefaultOptions(TimeSpan timeout)
        {
            return new TransactionOptions()
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = timeout == TimeSpan.MinValue ? TransactionManager.DefaultTimeout : timeout
            };
        }

        /// <summary>
        /// Indicates that all operations within the scope are completed successfully.
        /// </summary>
        /// <exception cref="System.InvalidOperationException"> 
        /// This method has already been called once.
        /// </exception>
        public void Complete()
        {
            this.scope.Complete();
        }

        /// <summary>
        ///  Ends the transaction scope.
        /// </summary>
        public void Dispose()
        {
            this.scope.Dispose();
        }
    }
}


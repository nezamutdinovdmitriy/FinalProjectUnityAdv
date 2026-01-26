using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class WalletService
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;

        public WalletService(Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies)
        {
            _currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>(currencies);
        }

        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList();

        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes currencyTypes) => _currencies[currencyTypes];

        public bool Enough(CurrencyTypes currencyTypes, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _currencies[currencyTypes].Value >= amount;
        }

        public void Add(CurrencyTypes currencyTypes, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[currencyTypes].Value += amount;
        }

        public void Spend(CurrencyTypes currencyTypes, int amount)
        {
            if (Enough(currencyTypes, amount) == false)
                throw new InvalidOperationException($"Not enough: {currencyTypes.ToString()}");

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[currencyTypes].Value -= amount;
        }
    }
}
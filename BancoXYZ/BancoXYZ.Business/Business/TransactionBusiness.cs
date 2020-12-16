using BancoXYZ.Business.Entities;
using BancoXYZ.Business.Models.Transaction;
using BancoXYZ.Business.Repository;
using System;

namespace BancoXYZ.Business.Business
{
    public class TransactionBusiness
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionBusiness(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public double GetSaldo(string accountNumber)
        {
            return _transactionRepository.GetSaldo(accountNumber);
        }

        public bool RegistrarMovimiento(MovimientoDTO movimientoDTO)
        {
            try
            {
                _transactionRepository.BeginTransaction();

                var cuenta = new Cuenta
                {
                    Id = 1,
                    Numero = movimientoDTO.NumeroCuenta
                };

                var movimiento = new Movimiento
                {
                    TipoMovimiento = movimientoDTO.TipoMovimiento,
                    Valor = movimientoDTO.Valor,
                    Cuenta = cuenta
                };

                movimiento = _transactionRepository.RegistrarMovimiento(movimiento);

                var saldo = new Saldo
                {
                    Cuenta = cuenta,
                    Estado = Types.EstadoType.Activo,
                    Movimiento = movimiento,
                    Valor = movimientoDTO.Valor,
                };

                _transactionRepository.RegistrarSaldo(saldo);

                _transactionRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                _transactionRepository.RollbackTransaction();
                return false;
            }

            return true;
        }
    }
}

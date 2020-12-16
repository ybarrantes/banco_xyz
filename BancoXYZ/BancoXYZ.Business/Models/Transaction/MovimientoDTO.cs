using BancoXYZ.Business.Types;
using System;

namespace BancoXYZ.Business.Models.Transaction
{
    [Serializable]
    public class MovimientoDTO
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }
        public TipoMovimientoType TipoMovimiento { get; set; }
    }
}

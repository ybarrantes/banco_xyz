using System;

namespace BancoXYZ.Business.Entities
{
    [Serializable]
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public Pais Pais { get; set; }
    }
}

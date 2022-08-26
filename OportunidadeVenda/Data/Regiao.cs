using System.ComponentModel.DataAnnotations;

namespace OportunidadeVenda.Data
{
    public enum Regiao
    {
      Norte, 
      Nordeste, 
      Sudeste, 
      Sul,
      [Display(Name = "Centro-Oeste")]
      CentroOeste
    }
}

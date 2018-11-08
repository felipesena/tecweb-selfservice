using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Model.Atributos;

namespace WebApp.Model
{
    public class Propriedade
    {
        public Colesterol Colesterol { get; set; }
        public Energia Energia { get; set; }
        public Ferro Ferro { get; set; }
        public Fibra Fibra { get; set; }
        public Proteina Proteina { get; set; }
    }
}

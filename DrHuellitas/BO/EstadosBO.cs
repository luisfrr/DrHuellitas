﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class EstadosBO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string nomPais { get; set; }
        public int idPais { get; set; }
    }
}
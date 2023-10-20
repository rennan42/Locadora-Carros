﻿using LocadoraCarros.Domain.Enum;

namespace LocadoraCarros.Domain.Entidades
{
    public class Veiculo : Entidade
    {
        public EModeloVeiculo Modelo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Placa { get; set; }
        public EStatusVeiculo Status { get; set;}
    }
}
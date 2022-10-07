using System;

namespace API_Folhas.Models
{
    public class Folha
    {
        // Construtor vazio
        public Folha(){ }
        public int FolhaId { get; set; }
        public string Ano {get; set;}
        public string Mes {get; set;}
        public string CpfFunc {get; set;}
        public double ValorHora { get; set;}
        public double QuantidaDedeHoras {get; set;}
        public double SalBruto {get; set;}
        public double SalLiq {get; set;}
        public double ImpostoRenda {get; set;}
        public double INSS {get; set;}
        public double FGTS {get; set;}
        public Funcionario funcionario {get; set;}
        
        public Folha(Funcionario funcionario){
            // Para imposto de renda
           if(this.SalBruto <= 1.903)
           {
            this.SalLiq = SalBruto;
            this.ImpostoRenda = 0.0;
           }else if(this.SalBruto >= 1.903 && this.SalBruto <= 2.826 )
           {
            this.SalLiq = this.SalBruto - (this.SalBruto - 0.075);
            this.ImpostoRenda = 0.075;
           }else if(this.SalBruto >= 2.826 && this.SalBruto <= 3.751)
           {
            this.SalLiq = this.SalBruto - (this.SalBruto - 0.15);
            this.ImpostoRenda = 0.015;
           }else if(this.SalBruto >= 3.751 && this.SalBruto <= 4664)
           {
               this.SalLiq = this.SalBruto - (this.SalBruto - 0.22);
               this.ImpostoRenda = 0.22;
           }else{
                this.SalLiq = this.SalBruto - (this.SalBruto - 0.27);
                this.ImpostoRenda = 0.27;
           }

           //Para INSS
           if(this.SalBruto <= 1.693)
           {
            this.SalLiq = this.SalBruto -( this.SalBruto * 0.08 );
            this.INSS = 0.08;
           }else if(this.SalBruto >= 1.693 && this.SalBruto <= 2.822 )
           {            
            this.SalLiq = this.SalLiq - (this.SalLiq - 0.09);
            this.INSS = 0.09;
           }else if(this.SalBruto >= 2.822 && this.SalBruto <= 5.645)
           {
            this.SalLiq = this.SalLiq - (this.SalLiq - 0.11);
            this.INSS = 0.11;
           }else if(this.SalBruto > 5.645){
               this.SalLiq = this.SalLiq - (this.SalLiq - 0.22);
               this.INSS = 0.22;
           }

           //Para FGTS
           this.SalLiq =  this.SalBruto - (this.SalBruto - 0.08);         
           this.SalLiq = this.SalBruto - this.INSS - this.ImpostoRenda;
        }
        //public int FuncionarioId { get; set; }
    }
}
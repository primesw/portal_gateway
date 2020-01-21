using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

/* Instalação de Packages necessários:

    - dotnet add package Newtonsoft.Json
    - dotnet add package Microsoft.AspNet.WebApi.Client

*/

namespace clientDotNet
{
/*Objeto criado para representar os dados
 que devem ser injetados no corpo do método POST*/
    class PostBody {
        public string employerDoc {get; set;}
        public string employeeIdType {get; set;}
        public string employeeId {get; set;}
        public string dataHoraInicio {get; set;}
        public string dataHoraTermino {get; set;}
    }

/****  INÍCIO - Estrutura de classes que formam a Folha ****/
    class LeafObject {
        public bool success {get; set;}
        public string message {get; set;}
        public string employerName {get; set;}
        public string employeeName {get; set;}
        public string cnpj {get; set;}
        public string pis {get; set;}
        public Folha leaf {get; set;}
    }

    class Folha {
        public List<Item> itens {get; set;}
        public Total total {get; set;}
    }

    class Item {
        public string data {get; set;}
        public string chp {get; set;}
        public string hw {get; set;}
        public string hwr {get; set;}
        public string hwn {get; set;}
        public string hnw {get; set;}
        public string hi {get; set;}
        public bool lack {get; set;}
        public bool hourBankUsing {get; set;}
        public string hourBankBalance {get; set;}
        public PaidWeeklyBreak paidWeeklyBreak {get; set;}
        public List<JustPart> justsPartial {get; set;}
        public List<Interval> intervals {get; set;}
        public List<Extra> extras {get; set;}
    }

    class Interval {
        public String dateTime {get; set;}
        public String rep {get; set;}
        public String type {get; set;}
        public String justification {get; set;}
    }

    class Extra {
        public int minutes {get; set;}
        public double percentage {get; set;}
        public bool nocturnal {get; set;}
        public bool bank {get; set;}
    }

    class JustPart {
        public int minutes {get; set;}
        public String description {get; set;}
        public String type {get; set;}
    }

    class Total {
        public string chp {get; set;}
        public string ht {get; set;}
        public string htr {get; set;}
        public string htn {get; set;}
        public string hnt {get; set;}
        public string hi {get; set;}
        public int faltas {get; set;}
        public string banco {get; set;}
        public bool bancoAuto {get; set;}
        public List<Extra> extras {get; set;}
        public PaidWeeklyBreak paidWeeklyBreak {get; set;}
    }

    class PaidWeeklyBreak {
        public int value {get; set;}
        public string mode {get; set;}
    }
    /****  FIM - Estrutura de classes que formam a Folha ****/

    class Program {   

    //Requisição
        static HttpClient client = new HttpClient();
        //Autenticação
        static async Task<LeafObject> getLeaf(PostBody pbody, string email, string senha) {
            var auth = Encoding.ASCII.GetBytes(email+":"+senha);
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers
                    .AuthenticationHeaderValue("Basic", Convert.ToBase64String(auth));
            HttpResponseMessage response =  
                await client.PostAsJsonAsync("/gateway/rest/leaf",pbody);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            //JSON para o Objeto Folha
            LeafObject obj = JsonConvert.DeserializeObject<LeafObject>(responseBody);
            return obj;
        }

        static async Task RunAsync() {
            client.BaseAddress = new Uri("https://portal.primesw.com.br");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));  
            try{
                //Dados do corpo do POST
                PostBody submitInfos = new PostBody();
                submitInfos.employerDoc = "03.539.681/0001-59";
                submitInfos.employeeIdType = "pis";
                submitInfos.employeeId = "16145178710";
                submitInfos.dataHoraInicio = "20190621";
                submitInfos.dataHoraTermino = "20190622";
                //Substituir e-mail e senha por seu acesso ao contexto PRIME:
                var authEmail = "seuEmail@mail.com";
                var authSenha = "sua_senha";
                //Execução
                var leaf = await getLeaf(submitInfos,authEmail,authSenha);
                //Convertendo o Objeto para Texto para exibição em console.
                string json = JsonConvert.SerializeObject(leaf);
                Console.WriteLine(json);
            }catch(Exception e){
                Console.WriteLine("Erro ao executar a requisição: "+e.Message);
            }  
        }

        static void Main(string[] args) {
            Console.WriteLine("Realizando a requisição....");
            RunAsync().GetAwaiter().GetResult();  
        }
    }
}

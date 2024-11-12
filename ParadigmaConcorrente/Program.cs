using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParadigmaConcorrente
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var numeros = Enumerable.Range(1, 1000).ToList(); // lista de numeros de 1 a 1000
            var resultados = await ProcessarNumerosAsync(numeros); //percorre em uma lista para verificar a execução de forma concorrente
            resultados.ForEach(Console.WriteLine);
        }

        static async Task<List<int>> ProcessarNumerosAsync(List<int> numeros)
        {
            
            //percorre a lista de numeros
            var tarefas = numeros.Select(async n =>
            {
                Console.WriteLine($"Iniciando tarefa {n} em {DateTime.Now:HH:mm:ss.fff}");
                await Task.Delay(100); //tarefas executadas de forma concorrente com o uso do async
                Console.WriteLine($"Finalizando tarefa {n} em {DateTime.Now:HH:mm:ss.fff}");
                return n;

            });

            //aguarda todas as tarefas serem concluidas
            return await Task.WhenAll(tarefas).ContinueWith(t => t.Result.ToList());
        
        }
    }
}

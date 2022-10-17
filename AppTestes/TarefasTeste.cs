using System;
using Xunit;

namespace AppTestes
{
    public class TarefasTeste
    {
        [Fact]
        public void ValidarEntradasDecamposTarefas()
        {
            //Arrange
            var tarefa = new TarefaDTO();
            //Act
            var nome = tarefa.Nome;
            //Assert
            Assert.True(!IsNullEmpty(nome));
        }
    }
}

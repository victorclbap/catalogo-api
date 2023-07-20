namespace catalogo_api.Pagination
{
    public class QueryStringParameters
    {

        const int maxPageSize = 50; // número máximo de registros qu vai retornar
        public int PageNumber { get; set; } = 1; // valor inicial da pagina é 1
        private int _pageSize = 10; // valor inicial é 10 registros


        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                // se o valor recebido for maior que o tamanho maximo de 50
                // o valor da pagina é setado 50
                _pageSize = (value > maxPageSize ? maxPageSize : value);
            }
        }

    }
}


namespace AtelieDaTransformacao.Desktop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            // Inicialização compatível com todas as versões de WinForms
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Evita ambiguidade com namespace 'Application' de outros projetos
            System.Windows.Forms.Application.Run(new Form1());
        }
    }
}
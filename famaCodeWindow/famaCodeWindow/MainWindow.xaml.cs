using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace famaCodeWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<famaCodeWindow.dataModels.Model> buttons;
        private Dictionary<famaCodeWindow.dataModels.Action.modelType,string> recordedClicks;
        private List<famaCodeWindow.dataModels.Action.modelType> mandatorieFields;
        public MainWindow()
        {
            InitializeComponent();
            this.mandatorieFields = new List<famaCodeWindow.dataModels.Action.modelType> { famaCodeWindow.dataModels.Action.modelType.Action, famaCodeWindow.dataModels.Action.modelType.Term };

            this.buttons = new List<dataModels.Model>();
            var actions = new List<famaCodeWindow.dataModels.Action>();
            this.recordedClicks = new Dictionary<dataModels.Model.modelType, string>();
            actions.Add(new dataModels.Action("bt001", "Recupercao bola",new List<string> { "bt002", "bt003", "bt004", "bt005" }));
            actions.Add(new dataModels.Action("bt002", "Perada bola", new List<string> { "bt001", "bt003", "bt004", "bt005" }));
            actions.Add(new dataModels.Action("bt003", "Falta", new List<string> { "bt002", "bt001", "bt004", "bt005" }));
            actions.Add(new dataModels.Action("bt004", "Remate", new List<string> { "bt002", "bt003", "bt001", "bt005" }));
            actions.Add(new dataModels.Action("bt005", "Passe", new List<string> { "bt002", "bt003", "bt004", "bt001" }));
            actions.Add(new dataModels.Action("ssffs", "O Paulinho", new List<string>()));
            actions.Add(new dataModels.Action("vdd", "é", new List<string>()));

            actions.Add(new dataModels.Action("ssdfgdffs", "GAY", new List<string>()));


            this.addActions(actions);
            var terms = new List<famaCodeWindow.dataModels.Term>();
            terms.Add(new dataModels.Term("trnm001", "1º Parte", new List<string> { "trnm002", "trnm997", "trnm998", "trnm999" })); 
            terms.Add(new dataModels.Term("trnm002", "2º Parte", new List<string> { "trnm001", "trnm997", "trnm998", "trnm999" })); 
            terms.Add(new dataModels.Term("trnm997", "1º Parte Prolongamento", new List<string> { "trnm001", "trnm002", "trnm998", "trnm999" }));
            terms.Add(new dataModels.Term("trnm998", "2º Parte Prolongamento", new List<string> { "trnm001", "trnm002", "trnm997", "trnm999" }));
            terms.Add(new dataModels.Term("trnm999", "Penaltis", new List<string> { "trnm001", "trnm002", "trnm997", "trnm998" }));

            this.addTerms(terms);
            submit.IsEnabled = false;
        }
        public void addActions(List<famaCodeWindow.dataModels.Action> actions)
        {
            Grid gridToadd = (Grid)mainGrid.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 0);

            foreach (famaCodeWindow.dataModels.Action action in actions)
            {
                this.buttons.Add(action);
                var newRowDef = new RowDefinition();
                gridToadd.RowDefinitions.Add(newRowDef);
                var actualRow = gridToadd.RowDefinitions.Count - 1;
                Button newBTN = new Button();
                newBTN.MaxHeight = 100;
                newBTN.MinHeight = 50;
                newBTN.MaxWidth = 150;
                newBTN.MinWidth = 50;
                newBTN.Margin = new Thickness(10, 10, 10, 10);
                newBTN.Content = action.text;
                gridToadd.Children.Add(newBTN);
                Grid.SetColumn(newBTN,0);
                Grid.SetRow(newBTN, actualRow);
                newBTN.Name = action.id;
                newBTN.Click += btnClick;
                action.assingedBTN= newBTN;
            }
        }



        public void addTerms(List<famaCodeWindow.dataModels.Term> terms)
        {
            Grid gridToadd = (Grid)mainGrid.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 6);

            foreach (famaCodeWindow.dataModels.Term term in terms)
            { 
                
                this.buttons.Add(term);
                var newRowDef = new RowDefinition();
                gridToadd.RowDefinitions.Add(newRowDef);
                var actualRow = gridToadd.RowDefinitions.Count - 1;
                Button newBTN = new Button();
                newBTN.MaxHeight = 100;
                newBTN.MinHeight = 50;
                newBTN.MaxWidth = 150;
                newBTN.MinWidth = 50;
                newBTN.Margin = new Thickness(10, 10, 10, 10);
                newBTN.Content = term.text;
                gridToadd.Children.Add(newBTN);
                Grid.SetColumn(newBTN, 0);
                Grid.SetRow(newBTN, actualRow);
                newBTN.Name = term.id;
                newBTN.Click += btnClick;
                term.assingedBTN = newBTN;
            }
        }


        private void btnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var model = this.buttons.Find(m => m.id.Equals(btn.Name));
            //btn.IsEnabled = false;
            btn.Background = Brushes.Green;
            try
            {
                this.recordedClicks.Add(model.type, model.id);

            }catch(Exception )
            {
                this.recordedClicks[model.type]= model.id;
            }
            foreach (var btnName in model.incompatibleBNTS)
            {
                var incBTN = this.buttons.Find(m => m.id.Equals(btnName)).assingedBTN;
                if (incBTN != null)
                {
                    incBTN.IsEnabled = false;
                }
                
            }
            if (this.canSummit())
            {
                submit.IsEnabled = true;
            }
            else
            {
                submit.IsEnabled = false;
            }
        }

        private bool canSummit()
        {

            foreach(var field in this.mandatorieFields)
            {
                if (!this.recordedClicks.ContainsKey(field)) return false;
            }
            return true;
        }
    }
}

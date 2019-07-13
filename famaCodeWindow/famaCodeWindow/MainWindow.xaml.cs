using famaCodeWindow.dataModels;
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
    /// 

    public class Moment : Model
    {
        public string text;

        public Moment(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Moment;
            this.text = text;
        }
    }

    public class Member : Model
    {
        public string text;

        public Member(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Moment;
            this.text = text;
        }
    }

    public class Zone : Model
    {
        public string text;

        public Zone(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Moment;
            this.text = text;
        }
    }

    public class Pass : Model
    {
        public string text;

        public Pass(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Pass;
            this.text = text;
        }
    }
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
           // actions.Add(new dataModels.Action("ssffs", "O Paulinho", new List<string>()));
           // actions.Add(new dataModels.Action("vdd", "é", new List<string>()));

            //actions.Add(new dataModels.Action("ssdfgdffs", "GAY", new List<string>()));


            this.addActions(actions);
            var terms = new List<famaCodeWindow.dataModels.Term>();
            terms.Add(new dataModels.Term("trnm001", "1º Parte", new List<string> { "trnm002", "trnm997", "trnm998", "trnm999" })); 
            terms.Add(new dataModels.Term("trnm002", "2º Parte", new List<string> { "trnm001", "trnm997", "trnm998", "trnm999" })); 
            terms.Add(new dataModels.Term("trnm997", "1º Parte Prolongamento", new List<string> { "trnm001", "trnm002", "trnm998", "trnm999" }));
            terms.Add(new dataModels.Term("trnm998", "2º Parte Prolongamento", new List<string> { "trnm001", "trnm002", "trnm997", "trnm999" }));
            terms.Add(new dataModels.Term("trnm999", "Penaltis", new List<string> { "trnm001", "trnm002", "trnm997", "trnm998" }));

            this.addTerms(terms);


            var moments = new List<Moment>();
            moments.Add(new Moment("m000", "OO", new List<string>()));
            moments.Add(new Moment("m001", "OD", new List<string>()));
            moments.Add(new Moment("m002", "TO", new List<string>()));
            //moments.Add(new Moment("m003", "TD", new List<string>()));
            this.addMoments(moments,2);


            var passes = new List<Pass>();
            passes.Add(new Pass("p000", "F", new List<string>()));
            passes.Add(new Pass("p001", "T/L", new List<string>()));
            passes.Add(new Pass("p002", "C", new List<string>()));
            passes.Add(new Pass("p003", "E", new List<string>()));
            this.addPassData(passes, 2);

            var zones = new List<Zone>();
            for(int i = 1; i < 25; i++)
            {
                if (i <= 12)
                {
                    zones.Add(new Zone("z00" + i, "Zone " + i, new List<string>()));

                }
                else
                {
                    zones.Add(new Zone("z00" + i, "A " + i, new List<string>()));

                }
            }
            /*zones.Add(new Zone("z0022", "Zone ", new List<string>()));
            zones.Add(new Zone("z0023", "Zone ", new List<string>()));
            zones.Add(new Zone("z0024", "Zone ", new List<string>()));*/


            this.addZone(zones, 3);

            var members = new List<Member>();
            for (int i = 1; i <= 8; i++)
            {
                members.Add(new Member("m00" + i, "Member " + i, new List<string>()));

            }
            this.addMember(members,2);
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



        public void addMoments(List<Moment> moments,int maxClos)
        {
            //this is grid on main window 
            Grid gridToadd = (Grid)mainGrid.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 2);
            //this is the top gid for moments buttons
            gridToadd = (Grid)gridToadd.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 0 && Grid.GetColumn(e) == 0);
            //var avaibleColluns = new List<ColumnDefinition>();
            for(int cCount=0; cCount < maxClos && cCount < moments.Count; cCount++)
            {
                var cdef = new ColumnDefinition();
                //avaibleColluns.Add(cdef);
                gridToadd.ColumnDefinitions.Add(cdef);

            }

            for (int mconut=0; mconut<moments.Count;)
            {
                //MOMENT ADDED TO BUTTONs list
                var newRowDef = new RowDefinition();
                gridToadd.RowDefinitions.Add(newRowDef);

                var actualRow = gridToadd.RowDefinitions.Count - 1;
                for (int workingC = 0;workingC< maxClos && workingC < moments.Count && mconut<moments.Count; workingC++)
                {
                    var moment = moments[mconut];
                    this.buttons.Add(moment);
                    /// bUTTON CREATION
                    Button newBTN = new Button();
                    newBTN.MaxHeight = 100;
                    newBTN.MinHeight = 20;
                    newBTN.MaxWidth = 150;
                    newBTN.MinWidth = 20;
                    newBTN.Margin = new Thickness(10, 10, 10, 10);
                    newBTN.Content = moment.text;
                    gridToadd.Children.Add(newBTN);
                    Grid.SetColumn(newBTN, workingC);
                    Grid.SetRow(newBTN, actualRow);
                    newBTN.Name = moment.id;
                    newBTN.Click += btnClick;
                    moment.assingedBTN = newBTN;
                    mconut++;
                }
                
            }
        }


        public void addPassData(List<Pass> moments, int maxClos)
        {
            //this is grid on main window 
            Grid gridToadd = (Grid)mainGrid.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 2);
            //this is the top gid for moments buttons
            gridToadd = (Grid)gridToadd.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 0);
            //var avaibleColluns = new List<ColumnDefinition>();
            for (int cCount = 0; cCount < maxClos && cCount < moments.Count; cCount++)
            {
                var cdef = new ColumnDefinition();
                //avaibleColluns.Add(cdef);
                gridToadd.ColumnDefinitions.Add(cdef);

            }

            for (int mconut = 0; mconut < moments.Count;)
            {
                //MOMENT ADDED TO BUTTONs list
                var newRowDef = new RowDefinition();
                gridToadd.RowDefinitions.Add(newRowDef);

                var actualRow = gridToadd.RowDefinitions.Count - 1;
                for (int workingC = 0; workingC < maxClos && workingC < moments.Count && mconut < moments.Count; workingC++)
                {
                    var moment = moments[mconut];
                    this.buttons.Add(moment);
                    /// bUTTON CREATION
                    Button newBTN = new Button();
                    newBTN.MaxHeight = 100;
                    newBTN.MinHeight = 20;
                    newBTN.MaxWidth = 150;
                    newBTN.MinWidth = 20;
                    newBTN.Margin = new Thickness(10, 10, 10, 10);
                    newBTN.Content = moment.text;
                    gridToadd.Children.Add(newBTN);
                    Grid.SetColumn(newBTN, workingC);
                    Grid.SetRow(newBTN, actualRow);
                    newBTN.Name = moment.id;
                    newBTN.Click += btnClick;
                    moment.assingedBTN = newBTN;
                    mconut++;
                }

            }
        }


        public void addZone(List<Zone> zones, int maxClos)
        {
            //this is grid on main window 
            Grid gridToadd = (Grid)mainGrid.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 4);
            //var avaibleColluns = new List<ColumnDefinition>();
            for (int cCount = 0; cCount < maxClos && cCount < zones.Count; cCount++)
            {
                var cdef = new ColumnDefinition();
                //avaibleColluns.Add(cdef);
                gridToadd.ColumnDefinitions.Add(cdef);

            }

            for (int mconut = 0; mconut < zones.Count;)
            {
                //MOMENT ADDED TO BUTTONs list
                var newRowDef = new RowDefinition();
                gridToadd.RowDefinitions.Add(newRowDef);

                var actualRow = gridToadd.RowDefinitions.Count - 1;
                for (int workingC = 0; workingC < maxClos && workingC < zones.Count && mconut < zones.Count; workingC++)
                {
                    var moment = zones[mconut];
                    this.buttons.Add(moment);
                    /// bUTTON CREATION
                    Button newBTN = new Button();
                    newBTN.MaxHeight = 100;
                    newBTN.MinHeight = 40;
                    newBTN.MaxWidth = 60;
                    newBTN.MinWidth = 60;
                    newBTN.Margin = new Thickness(5, 5, 5, 5);
                    newBTN.Content = moment.text;
                    gridToadd.Children.Add(newBTN);
                    Grid.SetColumn(newBTN, workingC);
                    Grid.SetRow(newBTN, actualRow);
                    newBTN.Name = moment.id;
                    newBTN.Click += btnClick;
                    moment.assingedBTN = newBTN;
                    mconut++;
                }

            }
        }


        public void addMember(List<Member> members, int maxClos)
        {
            //this is grid on main window 
            Grid gridToadd = (Grid)mainGrid.Children
                            .Cast<UIElement>()
                            .First(e => Grid.GetRow(e) == 1 && Grid.GetColumn(e) == 5);
            //var avaibleColluns = new List<ColumnDefinition>();
            for (int cCount = 0; cCount < maxClos && cCount < members.Count; cCount++)
            {
                var cdef = new ColumnDefinition();
                //avaibleColluns.Add(cdef);
                gridToadd.ColumnDefinitions.Add(cdef);

            }

            for (int mconut = 0; mconut < members.Count;)
            {
                //MOMENT ADDED TO BUTTONs list
                var newRowDef = new RowDefinition();
                gridToadd.RowDefinitions.Add(newRowDef);

                var actualRow = gridToadd.RowDefinitions.Count - 1;
                for (int workingC = 0; workingC < maxClos && workingC < members.Count && mconut < members.Count; workingC++)
                {
                    var moment = members[mconut];
                    this.buttons.Add(moment);
                    /// bUTTON CREATION
                    Button newBTN = new Button();
                    newBTN.MaxHeight = 100;
                    newBTN.MinHeight = 40;
                    newBTN.MaxWidth = 60;
                    newBTN.MinWidth = 60;
                    newBTN.Margin = new Thickness(5, 5, 5, 5);
                    newBTN.Content = moment.text;
                    gridToadd.Children.Add(newBTN);
                    Grid.SetColumn(newBTN, workingC);
                    Grid.SetRow(newBTN, actualRow);
                    newBTN.Name = moment.id;
                    newBTN.Click += btnClick;
                    moment.assingedBTN = newBTN;
                    mconut++;
                }

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

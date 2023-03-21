using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace TourPlanner_4_SWENII.ViewModels
{
    public class ClearCommandVM : ViewModelBase
    {
        /*
        
        public ClearCommandVM()
        
        {
            this.ClearCommand = new RelayCommand(

                (O) => !String.IsNullOrEmpty(SearchText) , //1.Paramater canexecute
                (O) => { this.SearchText = " " ; }//2.p execute
                
                     
                );

            SearchText = "First" ;

        
        
        }


        public RelayCommand ClearCommand   { get; set; }
      

        public string searchtext = "Tour";

        public string SearchText

            {

            get => searchtext;

            set
            
            {
                if(searchtext !=value)
                
                {
                    searchtext = value;
                    this.RaisePropertyChangedEvent();
                    this.ClearCommand.RaiseCanExecuteChanged();


                }



            }

            }




        */

    }
}

using BindingExercise.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BindingExercise.ViewModel
{
    public class PersonManagementVM
    {
        private readonly ICommand removeCommand;

        public ObservableCollection<PersonVM> People { get; set; }

        public ICommand AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    var person = new Person("Test", "Test");
                    var personVM = new PersonVM(person, this.removeCommand);
                    this.People.Add(personVM);
                });
            }
        }

        public PersonManagementVM()
        {
            this.removeCommand = new Command(obj =>
            {
                if (obj is PersonVM vm)
                {
                    this.People.Remove(vm);
                }
            });

            var people = new List<Person>()
            {
                new Person("Stefan", "Huber"),
                new Person("Thomas", "Mustermann"),
                new Person("Anna", "Gruber")
            };

            var vms = people.Select(p => new PersonVM(p, this.removeCommand));

            this.People = new ObservableCollection<PersonVM>(vms);
        }
    }
}

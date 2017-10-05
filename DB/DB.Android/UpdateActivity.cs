using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DB.Droid
{
    [Activity(Label = "UpdateActivity")]
    public class UpdateActivity : Activity
    {
        IPersonDAO _db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Update);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _db = MainActivity.Database;
            Person p = _db.Read(Intent.GetIntExtra("id", 0)) as Person;

            if (p != null)
            {
                FindViewById<TextView>(Resource.Id.editId).Text = p.Id.ToString();
                FindViewById<EditText>(Resource.Id.editFirstName).Text = p.FirstName;
                FindViewById<EditText>(Resource.Id.editLastName).Text = p.LastName;
                FindViewById<EditText>(Resource.Id.editAge).Text = p.Age.ToString();
            }
            FindViewById<Button>(Resource.Id.saveBtn).Click += delegate { OnSavePressed(); };
        }

        public void OnSavePressed()
        {
            Person person = new Person()
            {
                Id = int.Parse(FindViewById<TextView>(Resource.Id.editId).Text),
                FirstName = FindViewById<EditText>(Resource.Id.editFirstName).Text,
                LastName = FindViewById<EditText>(Resource.Id.editLastName).Text,
                Age = int.Parse(FindViewById<EditText>(Resource.Id.editAge).Text),
            };
            _db.Update(person);
            base.OnBackPressed();
        }
    }
}
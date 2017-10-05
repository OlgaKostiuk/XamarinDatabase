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
    [Activity(Label = "CreateActivity")]
    public class CreateActivity : Activity
    {
        IPersonDAO _db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Create);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _db = MainActivity.Database;

            FindViewById<Button>(Resource.Id.crBtn).Click += delegate { OnCreatePressed(); };
        }

        public void OnCreatePressed()
        {
            Person person = new Person()
            {
                Id = int.Parse(FindViewById<TextView>(Resource.Id.crId).Text),
                FirstName = FindViewById<EditText>(Resource.Id.crFirstName).Text,
                LastName = FindViewById<EditText>(Resource.Id.crLastName).Text,
                Age = int.Parse(FindViewById<EditText>(Resource.Id.crAge).Text),
            };
            _db.Create(person);
            base.OnBackPressed();
        }

    }
}
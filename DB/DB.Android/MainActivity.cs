using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using System.IO;

namespace DB.Droid
{
	[Activity (Label = "DB.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

        public static IPersonDAO Database { get; set; }
        static RecyclerView _recyclerView;
        public static MainActivity Instance { get; set; }

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            Instance = this;

            SetContentView(Resource.Layout.Main);

            Database = DBFactory.GetInstance("SQLite", GetDBPath());

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            Button btnCreate = FindViewById<Button>(Resource.Id.createPersonBtn);
            btnCreate.Click += delegate { StartActivity(typeof(CreateActivity)); };

            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            radioGroup.CheckedChange += RadioGroup_CheckedChange;
        }

        private void RadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            RadioButton radioButton = FindViewById<RadioButton>(e.CheckedId);
            Database = DBFactory.GetInstance(radioButton.Text, GetDBPath());
            ReadDataBase();
        }

        protected override void OnResume()
        {
            base.OnResume();
            ReadDataBase();
        }

        public static void ReadDataBase()
        {
            List<Person> people = Database.Read();
            _recyclerView.SetAdapter(new RecyclerAdapter(people));
        }

        public string GetDBPath()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "People.db");
            return path;
        }
    }
}



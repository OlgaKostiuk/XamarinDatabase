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
using Android.Support.V7.Widget;

namespace DB.Droid
{
    internal class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        private TextView _personId;
        private TextView _personFirstName;
        private TextView _personLastName;
        private TextView _personAge;

        protected RecyclerViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public RecyclerViewHolder(View itemView) : base(itemView)
        {
            _personId = itemView.FindViewById<TextView>(Resource.Id.personId);
            _personFirstName = itemView.FindViewById<TextView>(Resource.Id.personFirstName);
            _personLastName = itemView.FindViewById<TextView>(Resource.Id.personLastName);
            _personAge = itemView.FindViewById<TextView>(Resource.Id.personAge);

            var btnUpd = itemView.FindViewById<Button>(Resource.Id.updatePersonBtn);
            btnUpd.Click += delegate { UpdatePerson(itemView.Context); };

            var btnDel = itemView.FindViewById<Button>(Resource.Id.deletePersonBtn);
            btnDel.Click += delegate { DeletePerson(); };
        }

        private void DeletePerson()
        {
            MainActivity.Database.Delete(new Person(Convert.ToInt32(Id), FirstName, LastName, Convert.ToInt32(Age)));
            MainActivity.ReadDataBase();
        }

        private void UpdatePerson(Context context)
        {
            Intent intent = new Intent(context, typeof(UpdateActivity));
            intent.PutExtra("id", int.Parse(_personId.Text));
            context.StartActivity(intent);
        }

        public string Id
        {
            get => _personId.Text;
            set => _personId.Text = value;
        }

        public string FirstName
        {
            get => _personFirstName.Text;
            set => _personFirstName.Text = value;
        }

        public string LastName
        {
            get => _personLastName.Text;
            set => _personLastName.Text = value;
        }

        public string Age
        {
            get => _personAge.Text;
            set => _personAge.Text = value;
        }
    }
}
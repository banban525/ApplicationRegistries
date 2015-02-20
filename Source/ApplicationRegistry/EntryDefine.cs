using System;

namespace ApplicationRegistries
{
    class EntryDefine
    {
        private readonly string _id;
        private readonly TypeEnum _type;
        private readonly string _description;

        public EntryDefine(string id, TypeEnum type, string description)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (description == null) throw new ArgumentNullException("description");
            _id = id;
            _type = type;
            _description = description;
        }

        public string ID
        {
            get { return _id; }
        }

        public TypeEnum Type
        {
            get { return _type; }
        }

        public string Description
        {
            get { return _description; }
        }

        protected bool Equals(EntryDefine other)
        {
            return string.Equals(_id, other._id) && _type == other._type && string.Equals(_description, other._description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((EntryDefine) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_id != null ? _id.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) _type;
                hashCode = (hashCode*397) ^ (_description != null ? _description.GetHashCode() : 0);
                return hashCode;
            }
        }

        public EntryDefine Replace(string from, string to)
        {
            return new EntryDefine(_id.Replace(from, to), _type, _description.Replace(from, to));
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TC.Core
{
	public class InstanceContainer
	{
		private static readonly Dictionary<Type, object> typeInstanceMaps = new Dictionary<Type, object> ();
		private static readonly Dictionary<object, HashSet<Type>> instanceTypeMaps = new Dictionary<object, HashSet<Type>> ();

		public static void Bind (object instance)
		{
			if (instance == null) {
				return;
			}

			Bind (instance, false, instance.GetType ());
		}

		public static void Bind (object instance, params Type[] types)
		{
			if (instance == null) {
				return;
			}

			Bind (instance, false, types);
		}

		public void Rebind (object instance)
		{
			if (instance == null) {
				return;
			}

			Bind (instance, true, instance.GetType ());
		}

		public void Rebind (object instance, params Type[] types)
		{
			if (instance == null) {
				return;
			}

			Bind (instance, true, types);
		}

		private static void Bind (object instance, bool force, params Type[] types)
		{
			HashSet<Type> bindTypes;
			if (instanceTypeMaps.ContainsKey (instance)) {
				bindTypes = instanceTypeMaps [instance];
			} else {
				bindTypes = new HashSet<Type> ();
				instanceTypeMaps.Add (instance, bindTypes);
			}

			foreach (var type in types) {
				if (bindTypes.Contains (type)) {
					// type already bind to self
					continue;
				}

				if (typeInstanceMaps.ContainsKey (type)) {
					// type already bind to other
					var otherInstance = typeInstanceMaps [type];
					if (force) {
						Unbind (otherInstance, type);
					} else {
						Debug.LogWarningFormat (
							"InstanceContainer->Bind: can NOT bind type [{0}] to instance [{1}], because type ALREADY bind to other instance [{2}]",
							type.Name, instance.GetType ().Name, otherInstance.GetType ().Name);
						continue;
					}
				}

				// bind type to self
				typeInstanceMaps.Add (type, instance);
				bindTypes.Add (type);
			}
		}

		public static void Unbind (object instance)
		{
			if (instance == null) {
				return;
			}

			Unbind (instance, instance.GetType ());
		}

		public static void Unbind (object instance, params Type[] types)
		{
			if (instance == null) {
				return;
			}

			if (!instanceTypeMaps.ContainsKey (instance)) {
				// no binding
				return;
			}

			var bindTypes = instanceTypeMaps [instance];
			foreach (var type in types) {
				if (!bindTypes.Contains (type)) {
					continue;
				}

				if (typeInstanceMaps.ContainsKey (type)) {
					typeInstanceMaps.Remove (type);
				}
				bindTypes.Remove (type);
			}

			if (bindTypes.Count <= 0) {
				instanceTypeMaps.Remove (instance);
			}
		}

		public static void UnbindAll (object instance)
		{
			if (instance == null) {
				return;
			}

			if (!instanceTypeMaps.ContainsKey (instance)) {
				// no binding
				return;
			}

			var bindTypes = instanceTypeMaps [instance];
			foreach (var type in bindTypes) {
				if (typeInstanceMaps.ContainsKey (type)) {
					typeInstanceMaps.Remove (type);
				}
			}

			instanceTypeMaps.Remove (instance);
		}

		public static object Get (Type type)
		{
			return typeInstanceMaps.ContainsKey (type) ? typeInstanceMaps [type] : null;
		}

		public static T Get<T> ()
		{
			return (T) Get (typeof(T));
		}
	}
}
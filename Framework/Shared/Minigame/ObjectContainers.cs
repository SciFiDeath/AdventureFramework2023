namespace Framework.Minigames;


public class GameObjectContainer<T> where T : IGameObject
{
	private Dictionary<string, T> Elements { get; set; } = new();

	// Expose the most important methods of the dictionary
	public string[] Keys => Elements.Keys.ToArray();
	public T[] Values => Elements.Values.ToArray();
	public int Count => Elements.Count;
	public void Clear() => Elements.Clear();

	// Beware that there is no error handling here
	public T this[string key]
	{
		get => Elements[key];
		set => Elements[key] = value;
	}

	// TODO: Learn something about good exception handling
	public void Add(T element)
	{
		try
		{
			Elements.Add(element.Id, element);
			// this[element.Id] = element;
			element.OnKill += Kill;
		}
		catch (Exception e)
		{
			throw new Exception(
				$"Error adding element with id \"{element.Id}\" and type \"{element.GetType()}\"",
				e
			);
		}
	}

	public bool Remove(string name)
	{
		var element = Get(name);
		if (element != null)
		{
			element.OnKill -= Kill;
		}
		return Elements.Remove(name);
	}

	public T? Get(string name, T? defaultValue = default)
	{
		try
		{
			return Elements[name];
		}
		catch (KeyNotFoundException)
		{
			return defaultValue;
		}
	}

	public void Kill(object? sender, EventArgs e)
	{
		if (sender is T element)
		{
			Remove(element.Id);
		}
	}

	public void KillId(string id)
	{
		Elements[id].Kill();
	}

	// performs an action on each element
	// e.g. element => element.Visible = false; // hide all elements
	public void Transform(Action<T> action)
	{
		foreach (var element in Elements.Values)
		{
			action(element);
		}
	}

	public void KillAll()
	{
		Transform((e) => { e.Kill(); });
	}
}


// public class SVGElementContainer
// {
// 	private Dictionary<string, SVGElement> Elements { get; set; } = new();

// 	public string[] Keys => Elements.Keys.ToArray();
// 	public SVGElement[] Values => Elements.Values.ToArray();

// 	public int Count => Elements.Count;

// 	public SVGElement this[string key]
// 	{
// 		get => Elements[key];
// 		set => Elements[key] = value;
// 	}

// 	public void Kill(object? sender, EventArgs e)
// 	{
// 		if (sender is SVGElement element)
// 		{
// 			// Id cannot be null, as everything that's added receives an id
// 			Remove(element.Id!);
// 		}
// 	}

// 	public string Add(SVGElement element)
// 	{
// 		var id = element.Id ?? Guid.NewGuid().ToString("N");
// 		element.OnKill += Kill;
// 		Elements.Add(id, element);
// 		return id;
// 	}

// 	public SVGElement? Get(string name, SVGElement? defaultValue = default)
// 	{
// 		try
// 		{
// 			return Elements[name];
// 		}
// 		catch (KeyNotFoundException)
// 		{
// 			return defaultValue;
// 		}
// 	}

// 	public bool Remove(string name)
// 	{
// 		var element = Get(name);
// 		if (element != null)
// 		{
// 			element.OnKill -= Kill;
// 		}
// 		return Elements.Remove(name);
// 	}
// }


// //* Everything down here is a bit useless, but I like it nonetheless

// // This is kinda overkill, but that's what OOP is all about, right? ... r-right? ... RIGHT???
// public interface IStringId
// {
// 	public string Id { get; set; }
// }

// public interface IKillable : IStringId
// {
// 	public event EventHandler? OnKill;
// }

// public class KillableObjectContainer<T> : StringIdObjectContainer<T> where T : IKillable
// {
// 	public void Kill(object? sender, EventArgs e)
// 	{
// 		if (sender is T element)
// 		{
// 			Remove(element.Id!);
// 		}
// 	}

// 	public override string Add(T element)
// 	{
// 		element.OnKill += Kill;
// 		return base.Add(element);
// 	}

// 	public override bool Remove(string name)
// 	{
// 		var element = Get(name);
// 		if (element != null)
// 		{
// 			element.OnKill -= Kill;
// 		}
// 		return base.Remove(name);
// 	}
// }

// public class StringIdObjectContainer<T> where T : IStringId
// {
// 	private Dictionary<string, T> Elements { get; set; } = new();

// 	public string[] Keys => Elements.Keys.ToArray();
// 	public T[] Values => Elements.Values.ToArray();

// 	public int Count => Elements.Count;

// 	public T this[string key]
// 	{
// 		get => Elements[key];
// 		set => Elements[key] = value;
// 	}

// 	public virtual string Add(T element)
// 	{
// 		var id = element.Id ??
// 		Guid.NewGuid().ToString("N"); element.Id = id;
// 		Elements.Add(id, element);
// 		return id;
// 	}

// 	public T? Get(string name, T? defaultValue = default)
// 	{
// 		try
// 		{
// 			return Elements[name];
// 		}
// 		catch (KeyNotFoundException)
// 		{
// 			return defaultValue;
// 		}
// 	}

// 	public virtual bool Remove(string name)
// 	{
// 		return Elements.Remove(name);
// 	}
// }
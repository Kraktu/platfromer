using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class EnemiesManager : MonoBehaviour
{
	public TextAsset XMLFile;
	private EnemiesRoot enemiesXML;
	
	public class EnemiesRoot
	{
		public List<EnemyXML> Enemies;
	}
	 public class EnemyXML
	{
		public Vector2 _position;
		public float _speed;
		public bool _facingRight;
		public string _sentence;
		public ChickenTypeEnum MyChikenType;
	}

	private void Start()
	{
		XmlSerializer deserializer = new XmlSerializer(typeof(EnemyXML));
		StringReader reader = new StringReader(XMLFile.text);
		//EnemiesXML enemiesXML = (EnemiesXML)deserializer.Deserialize(reader);
	}
}

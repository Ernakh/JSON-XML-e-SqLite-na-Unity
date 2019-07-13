using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[DataContract]
public class User
{
    [DataMember]
    public string login;
    [DataMember]
    public string senha;
}

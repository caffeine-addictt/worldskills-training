package com.example.studentmobileapp.Models

import kotlinx.serialization.Serializable


@Serializable
public class Saloon {
  var saloonId: Int = 0
  var saloonName: String = ""
  var workshops: List<Workshop> = listOf()
}

@Serializable
public class GetSaloonResp {
  var status: Int = 0
  var message: String = ""
  var data: List<Saloon?> = listOf()
}

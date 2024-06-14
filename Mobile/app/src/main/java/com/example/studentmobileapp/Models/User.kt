package com.example.studentmobileapp.Models

import kotlinx.serialization.Serializable

@Serializable
class User {
  var userId: Int = 0
  var username: String = ""
  var fullname: String = ""
  var tel: Int = 0
}

@Serializable
class UserLoginReq {
  var username: String = ""
  var password: String = ""
}

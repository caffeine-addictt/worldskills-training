package com.example.studentmobileapp.Models

import kotlinx.serialization.Serializable


@Serializable
public class ApiResponse<T> {
  var status: Int = 0
  val message: String = ""
  val data: T = null!!
}

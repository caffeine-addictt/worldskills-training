package com.example.studentmobileapp.Models

import kotlinx.serialization.Serializable


@Serializable
public class Category {
  var categoryId: Int = 0
  var categoryName: String = ""
  var workshops: List<Workshop?> = listOf()
}

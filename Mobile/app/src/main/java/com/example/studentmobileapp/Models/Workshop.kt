package com.example.studentmobileapp.Models

import kotlinx.serialization.Serializable
import java.util.Date

@Serializable
class Workshop {
  var workshopId: Int = 0
  var title: String = ""
  var category: Category? = null
  var status: String = ""
  var requestDate: String = ""
  var timeslot: String = ""
  var endDate: String = ""
  var startDate: String = ""
  var saloon: Saloon? = null
}

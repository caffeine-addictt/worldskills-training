package com.example.studentmobileapp.Models

import kotlinx.serialization.Serializable


@Serializable
public class CreateWorkshopRequestReq
{
  var userId: Int = 0
  var title: String = ""
  var startDate: String = "" // yyyy-mm-dd
  var endDate: String = "" // yyyy-mm-dd
  var timeslot: Int = 0 // 0-10:12 1-12:14 2-14:16
  var category: String = ""
  var saloon: String = ""
}

@Serializable
public class CreateWorkshopRequestResp
{
  var status: Int = 0
  var message: String = ""
  var data: Workshop? = null!!
}

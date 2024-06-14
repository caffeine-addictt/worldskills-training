package com.example.studentmobileapp.Utilities

import android.os.AsyncTask
import com.example.studentmobileapp.Models.ApiResponse
import com.example.studentmobileapp.Models.CreateWorkshopRequestReq
import com.example.studentmobileapp.Models.CreateWorkshopRequestResp
import com.example.studentmobileapp.Models.Workshop
import com.example.studentmobileapp.WorkshopRequestAddFragment
import com.example.studentmobileapp.WorkshopRequestFragment
import com.example.studentmobileapp.databinding.WorkshopRequestViewBinding

class CreateWorkshopRequestHttpService(private var context: WorkshopRequestAddFragment, private var req: CreateWorkshopRequestReq): AsyncTask<Void, Void, String?>() {
  @Deprecated("Deprecated in Java")
  override fun doInBackground(vararg params: Void?): String? {
    val request = HttpPost<CreateWorkshopRequestResp, CreateWorkshopRequestReq>(
      "/api/workshop-request",
      req,
      listOf(201)
    )

    if (request == null) {
      return "Failed to create workshop request"
    }

    return null
  }

  @Deprecated("Deprecated in Java", ReplaceWith("super.onPreExecute()", "android.os.AsyncTask"))
  override fun onPreExecute() {
    super.onPreExecute()
  }

  @Deprecated("Deprecated in Java")
  override fun onPostExecute(result: String?) {
    super.onPostExecute(result)
    if(result == null) {
      context.onCreateSuccess()
    }
    else {
      context.onCreateError(result)
    }
  }
}

class GetWorkshopRequestHttpService(private var context: WorkshopRequestFragment, private var forRequest: Boolean): AsyncTask<Void, Void, List<Workshop>?>() {
  @Deprecated("Deprecated in Java")
  override fun doInBackground(vararg params: Void?): List<Workshop>? {
    val request = HttpGet<ApiResponse<List<Workshop>>>(
      "/api/workshop?userid=${Settings.user!!.userId}",
      listOf(200)
    )

    return request?.data
  }

  @Deprecated("Deprecated in Java", ReplaceWith("super.onPreExecute()", "android.os.AsyncTask"))
  override fun onPreExecute() {
    super.onPreExecute()
  }

  @Deprecated("Deprecated in Java")
  override fun onPostExecute(result: List<Workshop>?) {
    super.onPostExecute(result)
    if(result != null) {
      context.populateWorkshopRequests(result)
    }
    else {
      context.onFetchError()
    }
  }
}

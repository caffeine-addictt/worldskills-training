package com.example.studentmobileapp.Utilities

import android.os.AsyncTask
import android.util.Log
import com.example.studentmobileapp.Models.CreateWorkshopRequestReq
import com.example.studentmobileapp.Models.CreateWorkshopRequestResp
import com.example.studentmobileapp.Models.GetSaloonResp
import com.example.studentmobileapp.WorkshopRequestAddFragment
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.io.BufferedReader
import java.io.InputStreamReader
import java.io.OutputStream
import java.net.HttpURLConnection
import java.net.URL


class GetSaloonHttpService(private var context: WorkshopRequestAddFragment): AsyncTask<Void, Void, GetSaloonResp?>() {
  override fun doInBackground(vararg params: Void?): GetSaloonResp? {
    return HttpGet<GetSaloonResp>("/api/saloon")
  }

  override fun onPreExecute() {
    super.onPreExecute()
  }

  override fun onPostExecute(result: GetSaloonResp?) {
    super.onPostExecute(result)
    if(result != null) {
      context.populateSaloon(result.data)
    }
    else {
      context.onSaloonGetError()
    }
  }
}

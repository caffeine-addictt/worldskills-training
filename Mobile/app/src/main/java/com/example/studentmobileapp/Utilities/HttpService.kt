package com.example.studentmobileapp.Utilities

import android.util.Log
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.io.BufferedReader
import java.io.InputStreamReader
import java.io.OutputStream
import java.net.HttpURLConnection
import java.net.URL


const val BASE_URL = "http://10.0.2.2:5051"

inline fun <reified T, reified V>HttpPost(endpoint: String, req: V, parseStatus: List<Int> = listOf(200)): T? {
  val url = URL("$BASE_URL$endpoint")
  val con: HttpURLConnection = url.openConnection() as HttpURLConnection
  con.requestMethod = "POST"
  con.setRequestProperty("Content-Type", "application/json; utf-8");
  con.setRequestProperty("Accept", "application/json");

  val json = Json.encodeToString(req)
  val os: OutputStream = con.outputStream
  os.write(json.toByteArray())
  os.flush()
  os.close()
  Log.d("POST", "response code "+con.responseCode);

  if (con.responseCode !in parseStatus) {
    return null
  }

  val reader = BufferedReader(InputStreamReader(con.inputStream))
  val jsonData = StringBuilder()

  var line: String?
  while (reader.readLine().also { line = it } != null) {
    jsonData.append(line)
  }
  reader.close()
  con.disconnect();

  //Read JSON response and print
  try {
    val jsonL = Json{ ignoreUnknownKeys = true }
    val objJson: T = jsonL.decodeFromString<T>(jsonData.toString())

    Log.i("POST", "Successfully sent a post request to $endpoint")
    return objJson
  } catch (e: Exception) {
    Log.e("POST", "Failed to send a post request to $endpoint: $e")
  }

  return null
}

inline fun <reified T>HttpGet(endpoint: String, parseStatus: List<Int> = listOf(200)): T? {
  val url = URL("$BASE_URL$endpoint")
  val con: HttpURLConnection = url.openConnection() as HttpURLConnection
  con.requestMethod = "GET"
  con.setRequestProperty("Accept", "application/json");

  Log.d("GET", "response code "+con.responseCode);

  if (con.responseCode !in parseStatus) {
    return null
  }

  val reader = BufferedReader(InputStreamReader(con.inputStream))
  val jsonData = StringBuilder()

  var line: String?
  while (reader.readLine().also { line = it } != null) {
    jsonData.append(line)
  }
  reader.close()
  con.disconnect();

  //Read JSON response and print
  try {
    val jsonL = Json{ ignoreUnknownKeys = true }
    val objJson: T = jsonL.decodeFromString<T>(jsonData.toString())

    Log.i("GET", "Successfully sent a post request to $endpoint")
    return objJson
  } catch (e: Exception) {
    Log.e("GET", "Failed to send a post request to $endpoint: $e")
  }

  return null
}

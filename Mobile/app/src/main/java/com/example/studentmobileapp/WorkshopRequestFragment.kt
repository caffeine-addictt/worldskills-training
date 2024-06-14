package com.example.studentmobileapp

import android.app.Activity
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.example.studentmobileapp.Models.Workshop
import com.example.studentmobileapp.Utilities.GetWorkshopRequestHttpService
import com.example.studentmobileapp.databinding.WorkshopRequestViewBinding
import java.util.Locale


class WorkshopRequestFragment : Fragment() {
  private var _binding: WorkshopRequestViewBinding? = null
  private val binding get() = _binding!!

  override fun onCreateView(
    inflater: LayoutInflater, container: ViewGroup?,
    savedInstanceState: Bundle?
  ): View {
    _binding = WorkshopRequestViewBinding.inflate(inflater, container, false)
    return binding.root
  }

  override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
    super.onViewCreated(view, savedInstanceState)

    (requireActivity() as MainActivity).title = "Workshop Requests"
    GetWorkshopRequestHttpService(this, true).execute()
    binding.fab.setOnClickListener() {
      findNavController().navigate(R.id.action_WorkshopRequest_to_WorkshopRequestAdd)
    }
  }

  override fun onDestroyView() {
    super.onDestroyView()
    _binding = null
  }

  fun populateWorkshopRequests(workshopData: List<Workshop?>) {
    val myListAdapter = WorkshopRequestAdapter(requireActivity() as MainActivity, workshopData.filterNotNull())
    binding.wrList.adapter = myListAdapter
  }

  fun onFetchError() {
    Toast.makeText(requireActivity(), "Failed to fetch workshop request!", Toast.LENGTH_SHORT).show()
  }
}


class WorkshopRequestAdapter(private val context: Activity, private val workshopData: List<Workshop>): ArrayAdapter<String>(context, R.layout.workshop_request_listview_row, workshopData.map{ w -> w.title}) {
  override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
    val inflater = context.layoutInflater
    val rowView = inflater.inflate(R.layout.workshop_request_listview_row, null, true)

    // Get views
    val titleText: TextView = rowView.findViewById(R.id.header)
    val subtitleText: TextView = rowView.findViewById(R.id.text)

    val data = workshopData[position]
    var dateString = if (data.startDate == data.endDate) data.startDate else "${data.startDate} to ${data.endDate}"

    titleText.text = "[${data.status.uppercase(Locale.ROOT)}] ${data.title}"
    subtitleText.text = "${data.timeslot}\n$dateString\n@ ${data.saloon!!.saloonName}"

    return rowView
  }
}

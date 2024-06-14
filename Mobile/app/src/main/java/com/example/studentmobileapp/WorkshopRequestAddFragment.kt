package com.example.studentmobileapp

import android.app.DatePickerDialog
import android.icu.util.Calendar
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.example.studentmobileapp.Models.CreateWorkshopRequestReq
import com.example.studentmobileapp.Models.Saloon
import com.example.studentmobileapp.Utilities.CreateWorkshopRequestHttpService
import com.example.studentmobileapp.Utilities.GetSaloonHttpService
import com.example.studentmobileapp.Utilities.Settings
import com.example.studentmobileapp.databinding.WorkshopRequestAddViewBinding


class WorkshopRequestAddFragment : Fragment() {
  private var _binding: WorkshopRequestAddViewBinding? = null
  private val binding get() = _binding!!

  private var saloons: List<Saloon> = listOf()
  private var startDatePicked = ""
  private var endDatePicked = ""

  override fun onCreateView(
    inflater: LayoutInflater, container: ViewGroup?,
    savedInstanceState: Bundle?
  ): View {
    _binding = WorkshopRequestAddViewBinding.inflate(inflater, container, false)
    return binding.root
  }

  override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
    super.onViewCreated(view, savedInstanceState)
    (requireActivity() as MainActivity).title = "Create Request"

    // Set loading state
    GetSaloonHttpService(this).execute()
    binding.saloonSP.adapter = ArrayAdapter(
      requireActivity(),
      android.R.layout.simple_spinner_dropdown_item,
      listOf("Loading...")
    )

    // Handle date picker
    val startCal = Calendar.getInstance()
    val startDateSetListener = DatePickerDialog.OnDateSetListener { _, year, monthOfYear, dayOfMonth ->
      startCal.set(Calendar.YEAR, year)
      startCal.set(Calendar.MONTH, monthOfYear)
      startCal.set(Calendar.DAY_OF_MONTH, dayOfMonth)

      // Set in yyyy/mm/dd
      val moy = if (monthOfYear < 10)
        "0${monthOfYear + 1}"
      else "$monthOfYear"

      val dom = if (dayOfMonth < 10)
        "0${dayOfMonth + 1}"
      else "$dayOfMonth"

      val formattedDate = "$year-$moy-$dom"
      startDatePicked = formattedDate
      binding.startDateBtn.text = formattedDate
    }

    val endCal = Calendar.getInstance()
    val endDateSetListener = DatePickerDialog.OnDateSetListener { _, year, monthOfYear, dayOfMonth ->
      endCal.set(Calendar.YEAR, year)
      endCal.set(Calendar.MONTH, monthOfYear)
      endCal.set(Calendar.DAY_OF_MONTH, dayOfMonth)

      // Set in yyyy/mm/dd
      val moy = if (monthOfYear < 10)
        "0${monthOfYear + 1}"
        else "$monthOfYear"

      val dom = if (dayOfMonth < 10)
        "0${dayOfMonth + 1}"
        else "$dayOfMonth"

      val formattedDate = "$year-$moy-$dom"
      endDatePicked = formattedDate
      binding.endDateBtn.text = formattedDate
    }

    binding.startDateBtn.setOnClickListener {
      DatePickerDialog(
        requireActivity(),
        startDateSetListener,
        startCal.get(Calendar.YEAR),
        startCal.get(Calendar.MONTH),
        startCal.get(Calendar.DAY_OF_MONTH)
      ).show()
    }
    binding.endDateBtn.setOnClickListener {
      DatePickerDialog(
        requireActivity(),
        endDateSetListener,
        endCal.get(Calendar.YEAR),
        endCal.get(Calendar.MONTH),
        endCal.get(Calendar.DAY_OF_MONTH)
      ).show()
    }



    // Populate Timeslots
    val timeslots = resources.getStringArray(R.array.timeslots)
    val adapterT = ArrayAdapter(requireActivity(), android.R.layout.simple_spinner_dropdown_item, timeslots)
    binding.timeslotSP.adapter = adapterT

    // Populate Categories
    val categories = resources.getStringArray(R.array.categories)
    val adapterC = ArrayAdapter(requireActivity(), android.R.layout.simple_spinner_dropdown_item, categories)
    binding.categorySP.adapter = adapterC

    // Handle create
    binding.createBtn.setOnClickListener {
      if (saloons.isEmpty()) {
        Toast.makeText(requireActivity(), "Saloons are not yet populated", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (binding.titleET.text.isNullOrEmpty()) {
        Toast.makeText(requireActivity(), "Title is required!", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (binding.saloonSP.selectedItem == null) {
        Toast.makeText(requireActivity(), "Saloon is required!", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (binding.timeslotSP.selectedItem == null) {
        Toast.makeText(requireActivity(), "Timeslot is required!", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (startDatePicked.isEmpty()) {
        Toast.makeText(requireActivity(), "Start Date is required!", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (endDatePicked.isEmpty()) {
        Toast.makeText(requireActivity(), "Start Date is required!", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (endDatePicked < startDatePicked) {
        Toast.makeText(requireActivity(), "Start Date has to be before End Date", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      if (binding.saloonSP.selectedItem.toString() == "Loading...") {
        Toast.makeText(requireActivity(), "Invalid saloon!", Toast.LENGTH_SHORT).show()
        return@setOnClickListener
      }

      // Make request
      val createReq = CreateWorkshopRequestReq()
      createReq.userId = Settings.user!!.userId
      createReq.title = binding.titleET.text.toString()
      createReq.category = binding.categorySP.selectedItem.toString()
      createReq.startDate = startDatePicked
      createReq.endDate = endDatePicked
      createReq.saloon = binding.saloonSP.selectedItem.toString()

      CreateWorkshopRequestHttpService(this, createReq).execute()
    }
  }

  fun populateSaloon(saloonData: List<Saloon?>) {
    val saloonDataF = saloonData.filterNotNull()
    saloons = saloonDataF
    binding.saloonSP.adapter = ArrayAdapter(
      requireActivity(),
      android.R.layout.simple_spinner_dropdown_item,
      saloonDataF.map { s: Saloon -> s.saloonName }
    )
  }
  fun onSaloonGetError() {
    Toast.makeText(requireActivity(), "Failed to get saloons!", Toast.LENGTH_SHORT).show()
  }

  fun onCreateSuccess() {
    Toast.makeText(requireActivity(), "Successfully created new workshop request!", Toast.LENGTH_SHORT).show()
    findNavController().navigate(R.id.action_WorkshopRequestAdd_to_WorkshopRequest)
  }
  fun onCreateError(errText: String) {
    Toast.makeText(requireActivity(), errText, Toast.LENGTH_SHORT).show()
  }

  override fun onDestroyView() {
    super.onDestroyView()
    _binding = null
  }
}

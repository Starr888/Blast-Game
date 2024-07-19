using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace EasyMobile
{
	internal class AndroidNativeGif
	{
		private delegate void GifExportProgressDelegate(int taskId, float progress);

		private delegate void GifExportCompletedDelegate(int taskId, string filepath);

		private GifExportTask myExportTask;

		private static Dictionary<int, GCHandle[]> gcHandles;

		private static Dictionary<int, IntPtr[]> intPtrs;

		internal static event Action<int, float> GifExportProgress;

		internal static event Action<int, string> GifExportCompleted;

		internal AndroidNativeGif(GifExportTask exportTask)
		{
			myExportTask = exportTask;
		}

		[MonoPInvokeCallback(typeof(GifExportProgressDelegate))]
		private static void GifExportProgressCallback(int taskId, float progress)
		{
			if (AndroidNativeGif.GifExportProgress != null)
			{
				AndroidNativeGif.GifExportProgress(taskId, progress);
			}
		}

		[MonoPInvokeCallback(typeof(GifExportCompletedDelegate))]
		private static void GifExportCompletedCallback(int taskId, string filepath)
		{
			if (AndroidNativeGif.GifExportCompleted != null)
			{
				AndroidNativeGif.GifExportCompleted(taskId, filepath);
			}
			GCHandle[] array = gcHandles[taskId];
			foreach (GCHandle gCHandle in array)
			{
				gCHandle.Free();
			}
			for (int j = 0; j < intPtrs[taskId].Length; j++)
			{
				intPtrs[taskId][j] = IntPtr.Zero;
			}
			gcHandles.Remove(taskId);
			intPtrs.Remove(taskId);
		}

		[DllImport("easymobile")]
		private static extern void _ExportGif(int taskId, string filepath, int width, int height, int loop, int fps, int sampleFac, int frameCount, IntPtr[] imageData, GifExportProgressDelegate exportingCallback, GifExportCompletedDelegate exportCompletedCallback);

		internal static void ExportGif(GifExportTask exportTask)
		{
			AndroidNativeGif @object = new AndroidNativeGif(exportTask);
			Thread thread = new Thread(@object.DoExportGif);
			thread.Priority = exportTask.workerPriority;
			thread.Start();
		}

		private void DoExportGif()
		{
			int taskId = myExportTask.taskId;
			string filepath = myExportTask.filepath;
			int width = myExportTask.clip.Width;
			int height = myExportTask.clip.Height;
			int loop = myExportTask.loop;
			int framePerSecond = myExportTask.clip.FramePerSecond;
			int sampleFac = myExportTask.sampleFac;
			int frameCount = myExportTask.clip.Frames.Length;
			Color32[][] imageData = myExportTask.imageData;
			GCHandle[] array = new GCHandle[imageData.Length];
			IntPtr[] array2 = new IntPtr[imageData.Length];
			for (int i = 0; i < imageData.Length; i++)
			{
				array[i] = GCHandle.Alloc(imageData[i], GCHandleType.Pinned);
				array2[i] = array[i].AddrOfPinnedObject();
			}
			if (gcHandles == null)
			{
				gcHandles = new Dictionary<int, GCHandle[]>();
			}
			if (intPtrs == null)
			{
				intPtrs = new Dictionary<int, IntPtr[]>();
			}
			gcHandles.Add(taskId, array);
			intPtrs.Add(taskId, array2);
			_ExportGif(taskId, filepath, width, height, loop, framePerSecond, sampleFac, frameCount, intPtrs[taskId], GifExportProgressCallback, GifExportCompletedCallback);
		}
	}
}

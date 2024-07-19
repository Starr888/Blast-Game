using System;
using System.Collections.Generic;
using System.Threading;
using Moments.Encoder;

namespace Moments
{
	internal sealed class Worker
	{
		private Thread m_Thread;

		private int m_Id;

		internal List<GifFrame> m_Frames;

		internal GifEncoder m_Encoder;

		internal string m_FilePath;

		internal Action<int, string> m_OnFileSaved;

		internal Action<int, float> m_OnFileSaveProgress;

		internal Worker(int taskId, ThreadPriority priority, List<GifFrame> frames, GifEncoder encoder, string filepath, Action<int, float> onFileSaveProgress, Action<int, string> onFileSaved)
		{
			m_Id = taskId;
			m_Thread = new Thread(Run);
			m_Thread.Priority = priority;
			m_Frames = frames;
			m_Encoder = encoder;
			m_FilePath = filepath;
			m_OnFileSaved = onFileSaved;
			m_OnFileSaveProgress = onFileSaveProgress;
		}

		internal void Start()
		{
			m_Thread.Start();
		}

		private void Run()
		{
			m_Encoder.Start(m_FilePath);
			for (int i = 0; i < m_Frames.Count; i++)
			{
				GifFrame frame = m_Frames[i];
				m_Encoder.AddFrame(frame);
				if (m_OnFileSaveProgress != null)
				{
					float arg = (float)i / (float)m_Frames.Count;
					m_OnFileSaveProgress(m_Id, arg);
				}
			}
			m_Encoder.Finish();
			if (m_OnFileSaved != null)
			{
				m_OnFileSaved(m_Id, m_FilePath);
			}
		}
	}
}

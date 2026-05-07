using System;
using UnityEngine;

namespace Zenject
{
	[Serializable]
	[ZenjectAllowDuringValidation]
	[NoReflectionBaking]
	public class ZenjectSettings
	{
		[Serializable]
		public class SignalSettings
		{
			public static SignalSettings Default = new SignalSettings();

			[SerializeField]
			private SignalDefaultSyncModes _defaultSyncMode;

			[SerializeField]
			private SignalMissingHandlerResponses _missingHandlerDefaultResponse;

			[SerializeField]
			private bool _requireStrictUnsubscribe;

			[SerializeField]
			private int _defaultAsyncTickPriority;

			public int DefaultAsyncTickPriority => _defaultAsyncTickPriority;

			public SignalDefaultSyncModes DefaultSyncMode => _defaultSyncMode;

			public SignalMissingHandlerResponses MissingHandlerDefaultResponse => _missingHandlerDefaultResponse;

			public bool RequireStrictUnsubscribe => _requireStrictUnsubscribe;

			public SignalSettings(SignalDefaultSyncModes defaultSyncMode, SignalMissingHandlerResponses missingHandlerDefaultResponse = SignalMissingHandlerResponses.Warn, bool requireStrictUnsubscribe = false, int defaultAsyncTickPriority = 1)
			{
				_defaultSyncMode = defaultSyncMode;
				_missingHandlerDefaultResponse = missingHandlerDefaultResponse;
				_requireStrictUnsubscribe = requireStrictUnsubscribe;
				_defaultAsyncTickPriority = defaultAsyncTickPriority;
			}

			public SignalSettings()
				: this(SignalDefaultSyncModes.Synchronous)
			{
			}
		}

		public static ZenjectSettings Default = new ZenjectSettings();

		[SerializeField]
		private bool _ensureDeterministicDestructionOrderOnApplicationQuit;

		[SerializeField]
		private bool _displayWarningWhenResolvingDuringInstall;

		[SerializeField]
		private RootResolveMethods _validationRootResolveMethod;

		[SerializeField]
		private ValidationErrorResponses _validationErrorResponse;

		[SerializeField]
		private SignalSettings _signalSettings;

		public SignalSettings Signals => _signalSettings;

		public ValidationErrorResponses ValidationErrorResponse => _validationErrorResponse;

		public RootResolveMethods ValidationRootResolveMethod => _validationRootResolveMethod;

		public bool DisplayWarningWhenResolvingDuringInstall => _displayWarningWhenResolvingDuringInstall;

		public bool EnsureDeterministicDestructionOrderOnApplicationQuit => _ensureDeterministicDestructionOrderOnApplicationQuit;

		public ZenjectSettings(ValidationErrorResponses validationErrorResponse, RootResolveMethods validationRootResolveMethod = RootResolveMethods.NonLazyOnly, bool displayWarningWhenResolvingDuringInstall = true, bool ensureDeterministicDestructionOrderOnApplicationQuit = false, SignalSettings signalSettings = null)
		{
			_validationErrorResponse = validationErrorResponse;
			_validationRootResolveMethod = validationRootResolveMethod;
			_displayWarningWhenResolvingDuringInstall = displayWarningWhenResolvingDuringInstall;
			_ensureDeterministicDestructionOrderOnApplicationQuit = ensureDeterministicDestructionOrderOnApplicationQuit;
			_signalSettings = signalSettings ?? SignalSettings.Default;
		}

		public ZenjectSettings()
			: this(ValidationErrorResponses.Log)
		{
		}
	}
}

#pragma once

#include <Wiwa/core/Core.h>

#include <vector>
#include <Wiwa/ecs/EntityManager.h>
#include <Wiwa/physics/PhysicsManager.h>
#include <Wiwa/utilities/render/Camera.h>
#include <Wiwa/utilities/render/CameraManager.h>

#include <Wiwa/utilities/render/InstanceRenderer.h>

namespace Wiwa {
	class LightManager;
	class GuiManager;


	class WI_API Scene {
	public:
		Scene();
		~Scene();

		enum State {
			SCENE_ENTERING,
			SCENE_LOOP,
			SCENE_LEAVING
		};


		void Start();

		void Awake();
		void Init();
		void Update();

		void ModuleUpdate();

		void Load();
		void Unload(bool unload_resources=true);

		State getState() { return m_CurrentState; }

		void ChangeScene(size_t scene);

		EntityManager& GetEntityManager() { return m_EntityManager; }
		CameraManager& GetCameraManager() { return *m_CameraManager; }
		PhysicsManager& GetPhysicsManager() { return *m_PhysicsManager; }

		LightManager& GetLightManager() { return *m_LightManager; }
		GuiManager& GetGuiManager() { return *m_GuiManager; }
		InstanceRenderer& GetInstanceRenderer() { return m_InstanceRenderer; }
		inline const char* getName() { return m_Name.c_str(); }
		inline void ChangeName(const char* name) { m_Name = name; }
	protected:
		virtual void ProcessInput() {}

		virtual void UpdateEnter() {}
		virtual void UpdateLoop() {}
		virtual void UpdateLeave() {}

		virtual void RenderEnter() {}
		virtual void RenderLoop() {}
		virtual void RenderLeave() {}

		size_t mMaxTimeEntering, mMaxTimeLeaving = 0;

		EntityManager m_EntityManager;
		CameraManager* m_CameraManager;
		PhysicsManager* m_PhysicsManager;
		LightManager* m_LightManager;
		GuiManager* m_GuiManager;

	public:
	private:
		InstanceRenderer m_InstanceRenderer;

		State m_CurrentState = SCENE_ENTERING;
		size_t m_TransitionTimer = 0;
		size_t m_SceneToChange = 0;
		std::string m_Name = "Default scene";
	};
}
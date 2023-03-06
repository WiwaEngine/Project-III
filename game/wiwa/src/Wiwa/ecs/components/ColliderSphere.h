#pragma once
#pragma warning(disable : 4311)
#pragma warning(disable : 4302)
#include <Wiwa/core/Core.h>
#include <Wiwa/utilities/math/Vector3f.h>
#include <Wiwa/utilities/Reflection.h>

#include <glm/glm.hpp>

namespace Wiwa {
	struct WI_API ColliderSphere {
		float radius;
	};
}

REFLECTION_BEGIN(Wiwa::ColliderSphere)
	REFLECT_MEMBER(radius)
REFLECTION_END;
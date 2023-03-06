#pragma once
#include <glm/glm.hpp>
namespace Wiwa {
	typedef glm::vec3 Vector3f;
	typedef Vector3f Size3f;
	typedef Vector3f Color3f;

	namespace Vector3F {
		const Vector3f FRONT = { 0.0f, 0.0f, 1.0f };
		const Vector3f UP = { 0.0f, 1.0f, 0.0f };
	}
	/*struct Vector3f {
		union
		{
			struct { float x, y, z; };
			struct { float r, g, b; };
			struct { float s, t, p; };
		};
		Vector3f() = default;
		Vector3f(float x, float y, float z)
		{
			this->x = x;
			this->y = y;
			this->z = z;
		}

		inline Vector3f& operator+=(const Vector3f& v1) {
			x += v1.x;
			y += v1.y;
			z += v1.z;

			return *this;
		}

		inline Vector3f& operator-=(const Vector3f& v1) {
			x -= v1.x;
			y -= v1.y;
			z -= v1.z;

			return *this;
		}

		static Vector3f Zero() {
			return { 0.0f, 0.0f, 0.0f };
		}
	};

	inline Vector3f operator-(const Vector3f v1, const Vector3f v2) {
		return { v1.x - v2.x, v1.y - v2.y, v1.z - v2.z };
	}

	inline Vector3f operator+(const Vector3f v1, const Vector3f v2) {
		return { v1.x + v2.x, v1.y + v2.y, v1.z + v2.z };
	}
	
	inline bool operator==(const Vector3f v1, const Vector3f v2) {
		return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
	}

	inline bool operator!=(const Vector3f v1, const Vector3f v2) {
		return !(v1 == v2);
	}

	inline Vector3f operator*(const Vector3f v1, const Vector3f v2) {
		return { v1.x * v2.x, v1.y * v2.y, v1.z * v2.z };
	}

	typedef Vector3f Size3f;
	typedef Vector3f Color3f;

	namespace Vector3F {
		const Vector3f FRONT = { 0.0f, 0.0f, 1.0f };
		const Vector3f UP = { 0.0f, 1.0f, 0.0f };
	}*/
}